using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    public class BestellungAbschliessenCommand : Command<BestellungAggregate, BestellungId>
    {
        public BestellungAbschliessenCommand(BestellungId aggregateId) : base(aggregateId) { }
    }
}