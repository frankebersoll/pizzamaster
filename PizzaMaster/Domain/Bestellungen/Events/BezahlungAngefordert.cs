using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Bestellungen.Events
{
    public class BezahlungAngefordert : AggregateEvent<BestellungAggregate, BestellungId>
    {
        public BezahlungAngefordert(Benutzer benutzer, string beschreibung, Betrag betrag, DateTime datum)
        {
            this.Benutzer = benutzer;
            this.Betrag = betrag;
            this.Beschreibung = beschreibung;
            this.Datum = datum;
        }

        public Benutzer Benutzer { get; }

        public string Beschreibung { get; }

        public Betrag Betrag { get; }

        public DateTime Datum { get; }
    }
}