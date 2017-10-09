using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;

namespace PizzaMaster.Domain.Bestellungen.Events
{
    public class BestellungBegonnen : AggregateEvent<BestellungAggregate, BestellungId>
    {
        public BestellungBegonnen(string lieferdienst, DateTime datum)
        {
            this.Lieferdienst = lieferdienst;
            this.Datum = datum;
        }

        public DateTime Datum { get; }

        public string Lieferdienst { get; }
    }
}