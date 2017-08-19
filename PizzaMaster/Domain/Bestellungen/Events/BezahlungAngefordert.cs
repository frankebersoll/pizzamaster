using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Bestellungen.Events
{
    public class BezahlungAngefordert : AggregateEvent<BestellungAggregate, BestellungId>
    {
        public BezahlungAngefordert(Benutzer benutzer, string beschreibung, decimal betrag)
        {
            this.Benutzer = benutzer;
            this.Betrag = betrag;
            this.Beschreibung = beschreibung;
        }

        public Benutzer Benutzer { get; }

        public string Beschreibung { get; }

        public decimal Betrag { get; }
    }
}