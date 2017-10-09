using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace PizzaMaster.Tests.Bestellungen
{
    public class Zugeordnet : BestellungTestBase
    {
        public Zugeordnet(ITestOutputHelper output) : base(output)
        {
            this.Client.KontoEroeffnen(Benni).Einzahlen(20);
            this.Client.KontoEroeffnen(Frank);

            this.Bestellung
                .ArtikelHinzufuegen(6, "Chow-Fan Spezial", Frank)
                .ArtikelHinzufuegen(7, "Bangalisch Fisch Curry", Benni);
        }

        [Fact]
        public void Abbrechen()
        {
            this.Bestellung.Abbrechen();
            this.Bestellung.IstAbgeschlossen.Should().BeTrue();
            this.GetKonto(Frank).Transaktionen.Should().BeEmpty();
        }

        [Fact]
        public void Entfernen()
        {
            this.Bestellung.Artikel.First().Entfernen();
            this.Bestellung.Abschliessen();
            this.GetKonto(Frank).Transaktionen.Should().BeEmpty();
        }

        [Fact]
        public void Query()
        {
            this.Client.GetBestellungen().Should().ContainSingle();
        }
    }
}