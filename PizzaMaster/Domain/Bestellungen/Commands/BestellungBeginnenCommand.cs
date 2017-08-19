using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    public class BestellungBeginnenCommand : Command<BestellungAggregate, BestellungId>
    {
        public BestellungBeginnenCommand(string lieferdienst) : base(new BestellungId(BestellungId.New.Value))
        {
            if (string.IsNullOrWhiteSpace(lieferdienst))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(lieferdienst));

            this.Lieferdienst = lieferdienst;
        }

        public string Lieferdienst { get; }
    }
}