using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;

namespace PizzaMaster.Domain.Bestellungen.Events
{
    public class BestellungBegonnen : AggregateEvent<BestellungAggregate, BestellungId>
    {
        public BestellungBegonnen(string lieferdienst)
        {
            this.Lieferdienst = lieferdienst;
        }

        public string Lieferdienst { get; }
    }
}