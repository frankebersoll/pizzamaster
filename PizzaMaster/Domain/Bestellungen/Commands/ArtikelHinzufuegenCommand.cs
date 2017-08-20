using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    public class ArtikelHinzufuegenCommand : Command<BestellungAggregate, BestellungId>
    {
        public ArtikelHinzufuegenCommand(
            BestellungId aggregateId,
            Betrag betrag,
            string beschreibung,
            Benutzer benutzer = null)
            : base(aggregateId)
        {
            if (string.IsNullOrWhiteSpace(beschreibung))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(beschreibung));

            this.Betrag = betrag;
            this.Beschreibung = beschreibung;
            this.Benutzer = benutzer;
        }

        public Benutzer Benutzer { get; }

        public string Beschreibung { get; }

        public Betrag Betrag { get; }
    }
}