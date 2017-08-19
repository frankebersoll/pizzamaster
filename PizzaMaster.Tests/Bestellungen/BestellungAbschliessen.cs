using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Exceptions;
using FluentAssertions;
using PizzaMaster.Tests.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace PizzaMaster.Tests.Bestellungen
{
    public class BestellungAbschliessen : BestellungTestBase
    {
        public BestellungAbschliessen(ITestOutputHelper output, DatabaseFixture dbFixture) : base(output, dbFixture)
        {
            this.Client.KontoEroeffnen(Benni).Einzahlen(20);

            this.Bestellung
                .ArtikelHinzufuegen(7, "Bangalisch Fisch Curry", Benni)
                .ArtikelHinzufuegen(6, "Chow-Fan Spezial", Frank)
                .Abschliessen();
        }

        [Fact]
        public void AbschliessenKnallt()
        {
            Action action = () => this.Bestellung.Abschliessen();
            action.ShouldThrow<DomainError>();
        }

        [Fact]
        public void HinzufuegenKnallt()
        {
            Action action = () => this.Bestellung.ArtikelHinzufuegen(12, "Chicken Butter");
            action.ShouldThrow<DomainError>();
        }

        [Fact]
        public void Transaktionen()
        {
            var bestellungBeiEurasia = "Bestellung bei Eurasia";

            this.GetKonto(Benni)
                .TransaktionenShouldBe(Einzahlung(20, 20),
                                       Abbuchung(7, 13, bestellungBeiEurasia));

            this.GetKonto(Frank)
                .TransaktionenShouldBe(Abbuchung(6, -6, bestellungBeiEurasia));
        }
    }
}