using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;

namespace PizzaMaster.Domain.Bezahlen.Events
{
    public class BezahlungAbgeschlossen : AggregateEvent<BezahlungSaga, BezahlungId> { }
}