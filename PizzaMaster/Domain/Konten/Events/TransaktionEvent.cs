using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Konten.Events
{
    public abstract class TransaktionEvent : AggregateEvent<KontoAggregate, KontoId>
    {
        protected TransaktionEvent(Betrag betrag, decimal saldo)
        {
            this.Betrag = betrag;
            this.Saldo = saldo;
        }

        public Betrag Betrag { get; }

        public decimal Saldo { get; }
    }
}