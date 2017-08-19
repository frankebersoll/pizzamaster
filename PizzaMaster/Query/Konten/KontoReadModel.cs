using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using PizzaMaster.Domain.Common;
using PizzaMaster.Domain.Konten;
using PizzaMaster.Domain.Konten.Events;

namespace PizzaMaster.Query.Konten
{
    public class KontoReadModel : IVersionedReadModel,
                                  IAmReadModelFor<KontoAggregate, KontoId, KontoEroeffnet>,
                                  IAmReadModelFor<KontoAggregate, KontoId, Eingezahlt>,
                                  IAmReadModelFor<KontoAggregate, KontoId, Abgebucht>,
                                  IAmReadModelFor<KontoAggregate, KontoId, KontoAufgeloest>
    {
        public string Benutzer { get; private set; }

        public bool IsAufgeloest { get; private set; }

        public decimal Saldo { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<KontoAggregate, KontoId, Abgebucht> domainEvent)
        {
            this.Saldo = domainEvent.AggregateEvent.Saldo;
        }

        public void Apply(IReadModelContext context, IDomainEvent<KontoAggregate, KontoId, Eingezahlt> domainEvent)
        {
            this.Saldo = domainEvent.AggregateEvent.Saldo;
        }

        public void Apply(IReadModelContext context, IDomainEvent<KontoAggregate, KontoId, KontoAufgeloest> domainEvent)
        {
            this.IsAufgeloest = true;
        }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<KontoAggregate, KontoId, KontoEroeffnet> domainEvent)
        {
            this.Id = domainEvent.AggregateIdentity.Value;
            this.Benutzer = domainEvent.AggregateEvent.Benutzer.Value;
        }

        public string Id { get; private set; }

        public long? Version { get; set; }

        public Konto ToEntity()
        {
            return new Konto(new KontoId(this.Id), new Benutzer(this.Benutzer), this.Saldo);
        }
    }
}