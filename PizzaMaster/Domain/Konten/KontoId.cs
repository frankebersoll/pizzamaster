using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Core;

namespace PizzaMaster.Domain.Konten
{
    public class KontoId : Identity<KontoId>
    {
        public KontoId(string value) : base(value) { }
    }
}