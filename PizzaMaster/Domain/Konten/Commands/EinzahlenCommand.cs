using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Konten.Commands
{
    public class EinzahlenCommand : Command<KontoAggregate, KontoId>
    {
        public EinzahlenCommand(
            KontoId aggregateId,
            Betrag betrag,
            Einzahlungsart einzahlungsart = Einzahlungsart.Bareinzahlung) : base(aggregateId)
        {
            this.Einzahlungsart = einzahlungsart;
            this.Betrag = betrag;
        }

        public Betrag Betrag { get; }

        public Einzahlungsart Einzahlungsart { get; }
    }
}