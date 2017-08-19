using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Konten.Events
{
    public class KontoEroeffnet : AggregateEvent<KontoAggregate, KontoId>
    {
        public KontoEroeffnet(Benutzer benutzer)
        {
            this.Benutzer = benutzer;
        }

        public Benutzer Benutzer { get; }
    }
}