using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Domain.Common;
using PizzaMaster.Domain.Konten.Events;

namespace PizzaMaster.Domain.Konten
{
    public class KontoState : AggregateState<KontoAggregate, KontoId, KontoState>,
                              IApply<KontoEroeffnet>,
                              IApply<TransaktionEvent>,
                              IApply<Eingezahlt>,
                              IApply<Abgebucht>,
                              IApply<KontoAufgeloest>

    {
        public Benutzer Benutzer { get; private set; }

        public bool IstAufgeloest { get; private set; }

        public decimal Saldo { get; private set; }

        public void Apply(Abgebucht aggregateEvent)
        {
            this.Saldo = aggregateEvent.Saldo;
        }

        public void Apply(Eingezahlt aggregateEvent)
        {
            this.Saldo = aggregateEvent.Saldo;
        }

        public void Apply(KontoAufgeloest aggregateEvent)
        {
            this.IstAufgeloest = true;
        }

        public void Apply(KontoEroeffnet aggregateEvent)
        {
            this.Benutzer = aggregateEvent.Benutzer;
        }

        public void Apply(TransaktionEvent aggregateEvent)
        {
            this.Saldo = aggregateEvent.Saldo;
        }
    }
}