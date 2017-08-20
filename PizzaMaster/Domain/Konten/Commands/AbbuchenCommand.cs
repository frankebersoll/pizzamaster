using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;
using PizzaMaster.Domain.Bezahlen;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Konten.Commands
{
    public class AbbuchenCommand : Command<KontoAggregate, KontoId>
    {
        public AbbuchenCommand(
            KontoId aggregateId,
            Betrag betrag,
            string beschreibung,
            BezahlungId bezahlung = null)
            : base(aggregateId)
        {
            this.Betrag = betrag;
            this.Beschreibung = beschreibung;
            this.Bezahlung = bezahlung;
        }

        public string Beschreibung { get; }

        public Betrag Betrag { get; }

        public BezahlungId Bezahlung { get; }
    }
}