using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaster.Application.Client;
using Xunit.Abstractions;

namespace PizzaMaster.Tests.Konten
{
    public abstract class KontoTestBase : DomainTestBase
    {
        protected readonly KontoModel Konto;

        protected KontoTestBase(ITestOutputHelper output) : base(output)
        {
            this.Konto = this.CreateKonto();
        }

        protected virtual KontoModel CreateKonto()
        {
            return this.Client.KontoEroeffnen(Benni);
        }
    }
}