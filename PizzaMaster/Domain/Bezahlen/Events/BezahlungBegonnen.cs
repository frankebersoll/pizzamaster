using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;

namespace PizzaMaster.Domain.Bezahlen.Events
{
    public class BezahlungBegonnen : AggregateEvent<BezahlungSaga, BezahlungId>
    {
        public BezahlungBegonnen(string beschreibung, decimal betrag)
        {
            this.Betrag = betrag;
            this.Beschreibung = beschreibung;
        }

        public string Beschreibung { get; }

        public decimal Betrag { get; }
    }
}