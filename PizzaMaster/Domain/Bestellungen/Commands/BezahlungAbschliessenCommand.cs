using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;
using PizzaMaster.Domain.Bezahlen;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    public class BezahlungAbschliessenCommand : Command<BestellungAggregate, BestellungId>
    {
        public BezahlungAbschliessenCommand(BezahlungId bezahlung) : base(bezahlung.Bestellung)
        {
            this.Bezahlung = bezahlung;
        }

        public BezahlungId Bezahlung { get; }
    }
}