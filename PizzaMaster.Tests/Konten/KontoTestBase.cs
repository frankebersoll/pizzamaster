using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PizzaMaster.Application.Client;
using PizzaMaster.Query.Konten;
using Xunit.Abstractions;

namespace PizzaMaster.Tests.Konten
{
    public abstract class KontoTestBase : DomainTestBase
    {
        protected readonly KontoModel Konto;

        protected KontoTestBase(ITestOutputHelper output) : base(output)
        {
            AssertionOptions.AssertEquivalencyUsing(o =>
            {
                o.Using<Transaktion>(t => t.Subject.ShouldBeEquivalentTo(t.Expectation,
                                                                         x => x.Excluding(p => p.Timestamp)))
                 .WhenTypeIs<Transaktion>();
                return o;
            });

            this.Konto = this.CreateKonto();
        }

        protected virtual KontoModel CreateKonto()
        {
            return this.Client.KontoEroeffnen(Benni);
        }
    }
}