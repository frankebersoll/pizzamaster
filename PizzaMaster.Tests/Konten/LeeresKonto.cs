using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Exceptions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace PizzaMaster.Tests.Konten
{
    public class LeeresKonto : KontoTestBase
    {
        public LeeresKonto(ITestOutputHelper output, DatabaseFixture dbFixture) : base(output, dbFixture) { }

        [Fact]
        public void Abbuchen()
        {
            this.Konto.Abbuchen(20, "Verwendungszweck");
            this.Konto.Saldo.Should().Be(-20);
        }

        [Fact]
        public void DoppeltesKontoKnallt()
        {
            Action action = () => this.Client.KontoEroeffnen(Benni);
            action.ShouldThrow<DomainError>();
        }

        [Fact]
        public void Einzahlen()
        {
            this.Konto.Einzahlen(20);
            this.Konto.Saldo.Should().Be(20);
        }

        [Fact]
        public void QueryFindetKonto()
        {
            this.Client.GetKonten().Should().ContainSingle(k => k.Benutzer == this.Konto.Benutzer);
        }
    }
}