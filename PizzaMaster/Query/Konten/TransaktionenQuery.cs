using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Queries;
using PizzaMaster.Domain.Konten;

namespace PizzaMaster.Query.Konten
{
    public class TransaktionenQuery : IQuery<IEnumerable<Transaktion>>
    {
        public TransaktionenQuery(KontoId konto)
        {
            this.Konto = konto;
        }

        public KontoId Konto { get; }
    }
}