using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Queries;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Query.Konten
{
    public class KontoByBenutzerQuery : IQuery<KontoReadModel>
    {
        public KontoByBenutzerQuery(Benutzer benutzer, bool throwIfNotFound = true)
        {
            this.Benutzer = benutzer;
            this.ThrowIfNotFound = throwIfNotFound;
        }

        public Benutzer Benutzer { get; }

        public bool ThrowIfNotFound { get; }
    }
}