using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;

namespace PizzaMaster.Konten.Events {
    public class EingezahltEvent : AggregateEvent<KontoAggregate, KontoId>
    {
        public decimal Amount { get; }

        public EingezahltEvent(decimal amount)
        {
            this.Amount = amount;
        }
    }
}