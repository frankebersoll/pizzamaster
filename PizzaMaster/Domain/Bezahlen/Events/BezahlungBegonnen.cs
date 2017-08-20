using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Bezahlen.Events
{
    public class BezahlungBegonnen : AggregateEvent<BezahlungSaga, BezahlungId>
    {
        public BezahlungBegonnen(string beschreibung, Betrag betrag)
        {
            this.Betrag = betrag;
            this.Beschreibung = beschreibung;
        }

        public string Beschreibung { get; }

        public Betrag Betrag { get; }
    }
}