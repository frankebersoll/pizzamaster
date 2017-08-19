using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;
using PizzaMaster.Domain.Bezahlen;

namespace PizzaMaster.Domain.Konten.Commands
{
    public class AbbuchenCommand : Command<KontoAggregate, KontoId>
    {
        public AbbuchenCommand(
            KontoId aggregateId,
            decimal betrag,
            string beschreibung,
            BezahlungId bezahlung = null)
            : base(aggregateId)
        {
            this.Betrag = betrag;
            this.Beschreibung = beschreibung;
            this.Bezahlung = bezahlung;
        }

        public string Beschreibung { get; }

        public decimal Betrag { get; }

        public BezahlungId Bezahlung { get; }
    }
}