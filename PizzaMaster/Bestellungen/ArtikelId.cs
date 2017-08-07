using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Core;

namespace PizzaMaster.Bestellungen {
    public class ArtikelId : Identity<ArtikelId>
    {
        public ArtikelId(string value) : base(value) { }
    }
}