using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;

namespace PizzaMaster.Konten.Commands {
    public class EinzahlenCommand : Command<KontoAggregate, KontoId>
    {
        public decimal Amount { get; }

        public EinzahlenCommand(KontoId aggregateId, decimal amount) : base(aggregateId)
        {
            this.Amount = amount;
        }
    }
}