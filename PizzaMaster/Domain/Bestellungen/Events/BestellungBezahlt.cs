using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Bestellungen.Events
{
    public class BestellungBezahlt : AggregateEvent<BestellungAggregate, BestellungId>
    {
        public BestellungBezahlt(Benutzer benutzer)
        {
            this.Benutzer = benutzer;
        }

        public Benutzer Benutzer { get; }
    }
}