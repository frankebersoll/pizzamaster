using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Queries;
using PizzaMaster.Domain.Common;
using PizzaMaster.Domain.Konten;

namespace PizzaMaster.Query.Konten
{
    public class KontoByBenutzerQuery : IQuery<Konto>
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