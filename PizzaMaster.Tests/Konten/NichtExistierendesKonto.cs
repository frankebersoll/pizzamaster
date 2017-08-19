using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Exceptions;
using FluentAssertions;
using PizzaMaster.Application.Client;
using PizzaMaster.Domain.Konten;
using Xunit;
using Xunit.Abstractions;

namespace PizzaMaster.Tests.Konten
{
    public class NichtExistierendesKonto : KontoTestBase
    {
        public NichtExistierendesKonto(ITestOutputHelper output, DatabaseFixture dbFixture) :
            base(output, dbFixture) { }

        [Fact]
        public void AbbuchenKnallt()
        {
            Action action = () => this.Konto.Abbuchen(20, "test");
            action.ShouldThrow<DomainError>();
        }

        [Fact]
        public void AufloesenKnallt()
        {
            Action action = () => this.Konto.Aufloesen();
            action.ShouldThrow<DomainError>();
        }

        protected override KontoModel CreateKonto()
        {
            return new KontoModel(KontoId.New, this.Client);
        }

        [Fact]
        public void EinzahlenKnallt()
        {
            Action action = () => this.Konto.Einzahlen(20);
            action.ShouldThrow<DomainError>();
        }
    }
}