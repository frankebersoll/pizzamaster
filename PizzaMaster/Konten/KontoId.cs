using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Core;

namespace PizzaMaster.Konten {
    public class KontoId : Identity<KontoId>
    {
        public KontoId(string value) : base(value) { }
    }
}