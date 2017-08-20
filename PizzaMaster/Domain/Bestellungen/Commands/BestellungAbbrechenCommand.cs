using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    public class BestellungAbbrechenCommand : Command<BestellungAggregate, BestellungId>
    {
        public BestellungAbbrechenCommand(BestellungId id) : base(id) { }
    }
}