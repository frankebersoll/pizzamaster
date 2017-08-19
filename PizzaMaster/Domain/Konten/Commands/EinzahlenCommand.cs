using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;

namespace PizzaMaster.Domain.Konten.Commands
{
    public class EinzahlenCommand : Command<KontoAggregate, KontoId>
    {
        public EinzahlenCommand(KontoId aggregateId, decimal betrag) : base(aggregateId)
        {
            this.Betrag = betrag;
        }

        public decimal Betrag { get; }
    }
}