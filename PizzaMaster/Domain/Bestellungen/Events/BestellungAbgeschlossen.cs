using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;

namespace PizzaMaster.Domain.Bestellungen.Events
{
    public class BestellungAbgeschlossen : AggregateEvent<BestellungAggregate, BestellungId> { }
}