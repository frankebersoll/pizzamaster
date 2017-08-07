using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Konten.ValueObjects;

namespace PizzaMaster.Konten.Events {
    public class KontoEroeffnetEvent : AggregateEvent<KontoAggregate, KontoId>
    {
        public Benutzer Benutzer { get; }

        public KontoEroeffnetEvent(Benutzer benutzer)
        {
            this.Benutzer = benutzer;
        }
    }
}