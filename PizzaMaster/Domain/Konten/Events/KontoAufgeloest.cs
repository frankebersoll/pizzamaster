using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;

namespace PizzaMaster.Domain.Konten.Events
{
    public class KontoAufgeloest : AggregateEvent<KontoAggregate, KontoId> { }
}