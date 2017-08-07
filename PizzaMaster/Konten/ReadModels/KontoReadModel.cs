using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using PizzaMaster.Konten.Events;
using PizzaMaster.Konten.ValueObjects;

namespace PizzaMaster.Konten.ReadModels {
    public class KontoReadModel : IReadModel, 
                                  IAmReadModelFor<KontoAggregate, KontoId, KontoEroeffnetEvent>,
                                  IAmReadModelFor<KontoAggregate, KontoId, EingezahltEvent>,
                                  IAmReadModelFor<KontoAggregate, KontoId, KontoAufgeloest>
    {
        public Benutzer Benutzer { get; private set; }

        public decimal Saldo { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<KontoAggregate, KontoId, KontoEroeffnetEvent> domainEvent)
        {
            this.Id = domainEvent.AggregateIdentity;
            this.Benutzer = domainEvent.AggregateEvent.Benutzer;
        }

        public KontoId Id { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<KontoAggregate, KontoId, EingezahltEvent> domainEvent)
        {
            this.Saldo += domainEvent.AggregateEvent.Amount;
        }

        public Konto ToEntity()
        {
            return new Konto(this.Id, this.Benutzer, this.Saldo);
        }

        public void Apply(IReadModelContext context, IDomainEvent<KontoAggregate, KontoId, KontoAufgeloest> domainEvent)
        {
            this.IsAufgeloest = true;
        }

        public bool IsAufgeloest { get; private set; }
    }
}