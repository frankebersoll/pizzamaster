using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EventFlow.Sagas;
using EventFlow.ValueObjects;
using PizzaMaster.Domain.Bestellungen;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Bezahlen
{
    public class BezahlungId : ValueObject, ISagaId
    {
        private static readonly Regex Regex = new Regex("^(bestellung-[0-9a-f-]+)/(.+)$", RegexOptions.Compiled);

        public BezahlungId(BestellungId bestellung, Benutzer benutzer)
        {
            this.Bestellung = bestellung;
            this.Benutzer = benutzer;
        }

        public BezahlungId(string value)
        {
            var match = Regex.Match(value);
            if (!match.Success)
            {
                throw new ArgumentException("Could not parse id.");
            }

            this.Bestellung = new BestellungId(match.Groups[1].Value);
            this.Benutzer = new Benutzer(match.Groups[2].Value);
        }

        public Benutzer Benutzer { get; }

        public BestellungId Bestellung { get; }

        public static ISagaId Empty { get; } = new BezahlungId(null, null);

        public string Value => Equals(this, (BezahlungId) Empty)
                                   ? "Empty"
                                   : $"{this.Bestellung.Value}/{this.Benutzer.Value}";
    }
}