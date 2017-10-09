using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Domain.Bestellungen.Entities;
using PizzaMaster.Domain.Bestellungen.Events;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Bestellungen
{
    public class BestellungState : AggregateState<BestellungAggregate, BestellungId, BestellungState>,
                                   IApply<BestellungBegonnen>,
                                   IApply<ArtikelHinzugefuegt>,
                                   IApply<ArtikelEntfernt>,
                                   IApply<ArtikelBenutzerZugeordnet>,
                                   IApply<BezahlungAngefordert>,
                                   IApply<BestellungBezahlt>,
                                   IApply<BestellungAbgeschlossen>,
                                   IApply<BestellungAbgebrochen>
    {
        private readonly ArtikelList artikel = new ArtikelList();

        public IEnumerable<Artikel> Artikel => this.artikel.ToImmutableList();

        public DateTime Datum { get; private set; }

        public bool IstAbgeschlossen { get; private set; }

        public string Lieferdienst { get; private set; }

        public void Apply(ArtikelBenutzerZugeordnet e)
        {
            var id = e.ArtikelId;
            this.UpdateArtikel(id, a => a.Benutzer = e.Benutzer);
        }

        public void Apply(ArtikelEntfernt aggregateEvent)
        {
            this.artikel.Remove(aggregateEvent.ArtikelId);
        }

        public void Apply(ArtikelHinzugefuegt aggregateEvent)
        {
            this.artikel.Add(aggregateEvent.Artikel);
        }

        public void Apply(BestellungAbgebrochen aggregateEvent)
        {
            this.IstAbgeschlossen = true;
        }

        public void Apply(BestellungAbgeschlossen aggregateEvent)
        {
            this.IstAbgeschlossen = true;
        }

        public void Apply(BestellungBegonnen aggregateEvent)
        {
            this.Lieferdienst = aggregateEvent.Lieferdienst;
            this.Datum = aggregateEvent.Datum;
        }

        public void Apply(BestellungBezahlt e)
        {
            this.UpdateArtikel(e.Benutzer, a => a.Status = ArtikelStatus.Bezahlt);
        }

        public void Apply(BezahlungAngefordert e)
        {
            this.UpdateArtikel(e.Benutzer, a => a.Status = ArtikelStatus.BezahlungAngefordert);
        }

        public Artikel GetArtikel(ArtikelId artikelId) => this.artikel[artikelId];

        private void UpdateArtikel(ArtikelId id, Action<Artikel> action)
        {
            if (this.artikel.Contains(id))
            {
                action(this.artikel[id]);
            }
        }

        private void UpdateArtikel(Benutzer benutzer, Action<Artikel> action)
        {
            var artikel = this.artikel.Where(a => a.Benutzer == benutzer);
            foreach (var a in artikel)
            {
                action(a);
            }
        }
    }
}