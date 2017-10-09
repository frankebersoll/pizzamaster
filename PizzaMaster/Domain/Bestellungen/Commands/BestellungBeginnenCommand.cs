using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    public class BestellungBeginnenCommand : Command<BestellungAggregate, BestellungId>
    {
        public BestellungBeginnenCommand(string lieferdienst, DateTime datum = default(DateTime)) :
            base(new BestellungId(BestellungId.New.Value))
        {
            if (string.IsNullOrWhiteSpace(lieferdienst))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(lieferdienst));

            this.Lieferdienst = lieferdienst;
            this.Datum = datum;
        }

        public DateTime Datum { get; }

        public string Lieferdienst { get; }
    }
}