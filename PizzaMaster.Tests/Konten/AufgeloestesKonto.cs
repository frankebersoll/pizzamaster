using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Exceptions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace PizzaMaster.Tests.Konten
{
    public class AufgeloestesKonto : KontoTestBase
    {
        public AufgeloestesKonto(ITestOutputHelper output) : base(output)
        {
            this.Konto.Aufloesen();
        }

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

        [Fact]
        public void Benutzer()
        {
            this.Client.GetBenutzer().Should().BeEmpty();
        }

        [Fact]
        public void EinzahlenKnallt()
        {
            Action action = () => this.Konto.Einzahlen(20);
            action.ShouldThrow<DomainError>();
        }

        [Fact]
        public void QueryFindetNichts()
        {
            this.Client.GetKonten().Should().BeEmpty();
        }
    }
}