using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;

namespace PizzaMaster.Domain.Konten.Events
{
    public abstract class TransaktionEvent : AggregateEvent<KontoAggregate, KontoId>
    {
        protected TransaktionEvent(decimal betrag, decimal saldo)
        {
            this.Betrag = betrag;
            this.Saldo = saldo;
        }

        public decimal Betrag { get; }

        public decimal Saldo { get; }
    }
}