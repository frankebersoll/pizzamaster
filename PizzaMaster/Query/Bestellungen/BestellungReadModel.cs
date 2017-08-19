using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using PizzaMaster.Domain.Bestellungen;
using PizzaMaster.Domain.Bestellungen.Entities;
using PizzaMaster.Domain.Bestellungen.Events;

namespace PizzaMaster.Query.Bestellungen
{
    public class BestellungReadModel : IVersionedReadModel,
                                       IAmReadModelFor<BestellungAggregate, BestellungId, BestellungBegonnen>,
                                       IAmReadModelFor<BestellungAggregate, BestellungId, ArtikelHinzugefuegt>,
                                       IAmReadModelFor<BestellungAggregate, BestellungId, ArtikelBenutzerZugeordnet>,
                                       IAmReadModelFor<BestellungAggregate, BestellungId, BezahlungAngefordert>,
                                       IAmReadModelFor<BestellungAggregate, BestellungId, BestellungBezahlt>,
                                       IAmReadModelFor<BestellungAggregate, BestellungId, BestellungAbgeschlossen>
    {
        private readonly ArtikelList artikel = new ArtikelList();

        public IReadOnlyList<Artikel> Artikel => this.artikel.ToImmutableArray();

        public bool IstAbgeschlossen { get; private set; }

        public string Lieferdienst { get; private set; }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<BestellungAggregate, BestellungId, ArtikelBenutzerZugeordnet> domainEvent)
        {
            var zugeordnet = domainEvent.AggregateEvent;
            this.artikel[zugeordnet.ArtikelId].Benutzer = zugeordnet.Benutzer;
        }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<BestellungAggregate, BestellungId, ArtikelHinzugefuegt> domainEvent)
        {
            var hinzugefuegt = domainEvent.AggregateEvent;
            this.artikel.Add(hinzugefuegt.Artikel);
        }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<BestellungAggregate, BestellungId, BestellungAbgeschlossen> domainEvent)
        {
            this.IstAbgeschlossen = true;
        }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<BestellungAggregate, BestellungId, BestellungBegonnen> domainEvent)
        {
            this.Lieferdienst = domainEvent.AggregateEvent.Lieferdienst;
        }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<BestellungAggregate, BestellungId, BestellungBezahlt> domainEvent)
        {
            var bezahlt = domainEvent.AggregateEvent;
            foreach (var artikel in this.artikel.Where(a => a.Benutzer == bezahlt.Benutzer))
            {
                artikel.Status = ArtikelStatus.Bezahlt;
            }
        }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<BestellungAggregate, BestellungId, BezahlungAngefordert> domainEvent)
        {
            var angefordert = domainEvent.AggregateEvent;
            foreach (var artikel in this.artikel.Where(a => a.Benutzer == angefordert.Benutzer))
            {
                artikel.Status = ArtikelStatus.BezahlungAngefordert;
            }
        }

        public string Id { get; }

        public long? Version { get; set; }
    }
}