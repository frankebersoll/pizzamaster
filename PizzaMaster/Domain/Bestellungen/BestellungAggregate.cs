using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.Extensions;
using PizzaMaster.Domain.Bestellungen.Entities;
using PizzaMaster.Domain.Bestellungen.Events;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Bestellungen
{
    public class BestellungAggregate : AggregateRoot<BestellungAggregate, BestellungId>, IAggregateRoot
    {
        private readonly BestellungState state = new BestellungState();

        public BestellungAggregate(BestellungId id) : base(id)
        {
            this.Register(this.state);
        }

        private IEnumerable<Artikel> Artikel => this.state.Artikel;

        public bool IstAbgeschlossen => this.state.IstAbgeschlossen;

        public void Abschliessen()
        {
            BestellungSpecs.NichtAbgeschlossen
                           .And(Specs.Existiert)
                           .ThrowDomainErrorIfNotStatisfied(this);

            ArtikelSpecs.Zugeordnet
                        .And(ArtikelSpecs.Status(ArtikelStatus.Offen))
                        .ForAll()
                        .ThrowDomainErrorIfNotStatisfied(this.Artikel);

            var zugeordneteArtikel = (from artikel in this.Artikel
                                      group artikel by artikel.Benutzer
                                      into artikelNachBenutzer
                                      select artikelNachBenutzer).ToArray();

            foreach (var artikelNachBenutzer in zugeordneteArtikel)
            {
                var beschreibung = $"Bestellung bei {this.state.Lieferdienst}";
                var betrag = artikelNachBenutzer.Sum(a => a.Betrag);
                var e = new BezahlungAngefordert(artikelNachBenutzer.Key, beschreibung, betrag);
                this.Emit(e);
            }

            if (!zugeordneteArtikel.Any())
            {
                this.Emit(new BestellungAbgeschlossen());
            }
        }

        public void ArtikelHinzufuegen(decimal betrag, string beschreibung, Benutzer benutzer = null)
        {
            BestellungSpecs.NichtAbgeschlossen
                           .And(Specs.Existiert)
                           .ThrowDomainErrorIfNotStatisfied(this);

            var artikel = new Artikel(ArtikelId.New, betrag, beschreibung, benutzer);
            this.Emit(new ArtikelHinzugefuegt(artikel));
        }

        public void ArtikelZuordnen(ArtikelId artikelId, Benutzer benutzer)
        {
            var artikel = this.state.GetArtikel(artikelId);
            ArtikelSpecs.Status(ArtikelStatus.Offen)
                        .ThrowDomainErrorIfNotStatisfied(artikel);

            this.Emit(new ArtikelBenutzerZugeordnet(artikelId, benutzer));
        }

        public void Beginnen(string lieferdienst)
        {
            Specs.IstNeu.ThrowDomainErrorIfNotStatisfied(this);

            this.Emit(new BestellungBegonnen(lieferdienst));
        }

        public void BezahlungAbschliessen(Benutzer benutzer)
        {
            Specs.Existiert.ThrowDomainErrorIfNotStatisfied(this);

            var artikel = this.Artikel.Where(a => a.Benutzer == benutzer);
            ArtikelSpecs.Zugeordnet
                        .And(ArtikelSpecs.Status(ArtikelStatus.BezahlungAngefordert))
                        .ForAll()
                        .ThrowDomainErrorIfNotStatisfied(artikel);

            this.Emit(new BestellungBezahlt(benutzer));

            if (this.Artikel.All(a => a.Status == ArtikelStatus.Bezahlt))
            {
                this.Emit(new BestellungAbgeschlossen());
            }
        }
    }
}