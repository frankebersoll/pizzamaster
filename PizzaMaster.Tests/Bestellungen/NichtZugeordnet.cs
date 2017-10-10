using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Exceptions;
using FluentAssertions;
using PizzaMaster.Domain.Bestellungen.Commands;
using Xunit;
using Xunit.Abstractions;

namespace PizzaMaster.Tests.Bestellungen
{
    public class NichtZugeordnet : BestellungTestBase
    {
        public NichtZugeordnet(ITestOutputHelper output) : base(output)
        {
            this.Client.KontoEroeffnen(Benni).Einzahlen(20);

            this.Bestellung
                .ArtikelHinzufuegen(7, "Bangalisch Fisch Curry")
                .ArtikelHinzufuegen(6, "Chow-Fan Spezial");
        }

        [Fact]
        public void AbschliessenKnallt()
        {
            Action action = () => this.Bestellung.Abschliessen();
            action.ShouldThrow<DomainError>();
        }

        [Fact]
        public void ZuordnenOhneBenutzerKnallt()
        {
            Action action = () => this.Bestellung.Artikel[0].Zuordnen(null);
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ZuordnenOhneArtikelKnallt()
        {
            Action action = () => new ArtikelZuordnenCommand(this.Bestellung.Id, null, Benni);
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Zuordnen()
        {
            foreach (var artikel in this.Bestellung.Artikel)
            {
                artikel.Zuordnen(Benni);
            }

            this.Bestellung.Abschliessen();

            this.Bestellung.IstAbgeschlossen.Should().BeTrue();
            this.GetKonto(Benni).Saldo.Should().Be(7);
        }
    }
}