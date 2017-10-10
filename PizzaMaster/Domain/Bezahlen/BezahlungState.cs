using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Domain.Bezahlen.Events;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Bezahlen
{
    public class BezahlungState : AggregateState<BezahlungSaga, BezahlungId, BezahlungState>,
                                  IApply<BezahlungBegonnen>,
                                  IApply<KontoZugeordnet>,
                                  IApply<BezahlungAbgeschlossen>
    {
        public string Beschreibung { get; private set; }

        public Betrag Betrag { get; private set; }

        public void Apply(BezahlungAbgeschlossen aggregateEvent) { }

        public void Apply(BezahlungBegonnen aggregateEvent)
        {
            this.Beschreibung = aggregateEvent.Beschreibung;
            this.Betrag = aggregateEvent.Betrag;
        }

        public void Apply(KontoZugeordnet aggregateEvent)
        {
        }
    }
}