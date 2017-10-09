using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using PizzaMaster.Domain.Bestellungen;
using PizzaMaster.Domain.Bestellungen.Events;

namespace PizzaMaster.Query.Bestellungen
{
    public class BestellungReadModel : IVersionedReadModel,
                                       IAmReadModelFor<BestellungAggregate, BestellungId, BestellungBegonnen>,
                                       IAmReadModelFor<BestellungAggregate, BestellungId, ArtikelHinzugefuegt>,
                                       IAmReadModelFor<BestellungAggregate, BestellungId, ArtikelEntfernt>,
                                       IAmReadModelFor<BestellungAggregate, BestellungId, ArtikelBenutzerZugeordnet>,
                                       IAmReadModelFor<BestellungAggregate, BestellungId, BestellungAbgeschlossen>,
                                       IAmReadModelFor<BestellungAggregate, BestellungId, BestellungAbgebrochen>
    {
        public ArtikelReadModelCollection Artikel { get; private set; } = new ArtikelReadModelCollection();

        public DateTime Datum { get; private set; }

        public bool IstAbgeschlossen { get; private set; }

        public string Lieferdienst { get; private set; }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<BestellungAggregate, BestellungId, ArtikelBenutzerZugeordnet> domainEvent)
        {
            var zugeordnet = domainEvent.AggregateEvent;
            this.Artikel[zugeordnet.ArtikelId.Value].Benutzer = zugeordnet.Benutzer;
        }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<BestellungAggregate, BestellungId, ArtikelEntfernt> domainEvent)
        {
            var artikelEntfernt = domainEvent.AggregateEvent;
            this.Artikel.Remove(artikelEntfernt.ArtikelId.Value);
        }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<BestellungAggregate, BestellungId, ArtikelHinzugefuegt> domainEvent)
        {
            var hinzugefuegt = domainEvent.AggregateEvent;
            this.Artikel.Add(new ArtikelReadModel(hinzugefuegt.Artikel));
        }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<BestellungAggregate, BestellungId, BestellungAbgebrochen> domainEvent)
        {
            this.IstAbgeschlossen = true;
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
            this.Id = domainEvent.AggregateIdentity.Value;
            this.Lieferdienst = domainEvent.AggregateEvent.Lieferdienst;
            this.Datum = domainEvent.AggregateEvent.Datum;
        }

        public string Id { get; private set; }

        public long? Version { get; set; }
    }
}