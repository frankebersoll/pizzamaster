using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Core;

namespace PizzaMaster.Domain.Bestellungen
{
    public class BestellungId : Identity<BestellungId>
    {
        public BestellungId(string value) : base(value) { }
    }
}