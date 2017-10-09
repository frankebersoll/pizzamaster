using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Exceptions;
using FluentAssertions;
using PizzaMaster.Tests.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace PizzaMaster.Tests.Konten
{
    public class KontoImPlus : KontoTestBase
    {
        public KontoImPlus(ITestOutputHelper output) : base(output)
        {
            this.Konto.Einzahlen(20);
        }

        [Fact]
        public void Abbuchen()
        {
            this.Konto.Abbuchen(20, "Verwendungszweck");
            this.Konto.Saldo.Should().Be(0);
            this.Konto.TransaktionenShouldBe(Einzahlung(20, 20),
                                             Abbuchung(20, 0, "Verwendungszweck"));
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
            this.Client.GetBenutzer().ShouldBeEquivalentTo(new[] {Benni});
        }

        [Fact]
        public void Einzahlen()
        {
            this.Konto.Einzahlen(20);

            this.Konto.Saldo.Should().Be(40);
            this.Konto.TransaktionenShouldBe(Einzahlung(20, 20),
                                             Einzahlung(20, 40));
        }

        [Fact]
        public void NegativenBetragAbbuchenKnallt()
        {
            Action action = () => this.Konto.Abbuchen(-5, "Knödel");
            action.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void NegativenBetragEinzahlenKnallt()
        {
            Action action = () => this.Konto.Einzahlen(-5);
            action.ShouldThrow<ArgumentOutOfRangeException>();
        }
    }
}