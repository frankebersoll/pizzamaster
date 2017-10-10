using System;
using FluentAssertions;
using PizzaMaster.Domain.Bestellungen.Commands;
using Xunit;
using Xunit.Abstractions;

namespace PizzaMaster.Tests.Bestellungen
{
    public class LeereBestellung : BestellungTestBase
    {
        public LeereBestellung(ITestOutputHelper output) : base(output) { }

        [Fact]
        public void Abschliessen()
        {
            this.Bestellung.Abschliessen();
            this.Bestellung.IstAbgeschlossen.Should().BeTrue();
        }

        [Fact]
        public void Abbrechen()
        {
            this.Bestellung.Abbrechen();
            this.Bestellung.IstAbgeschlossen.Should().BeTrue();
        }

        [Fact]
        public void BestellenOhneLieferdienstKnallt()
        {
            Action action = () => this.Client.BestellungBeginnen("");
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void EntfernenOhneArtikelKnallt()
        {
            Action action = () => new ArtikelEntfernenCommand(this.Bestellung.Id, null);
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void HinzufuegenOhneBeschreibungKnallt()
        {
            Action action = () => this.Bestellung.ArtikelHinzufuegen(10, "");
            action.ShouldThrow<ArgumentException>();
        }
    }
}