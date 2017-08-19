using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Application;
using PizzaMaster.Application.Client;
using PizzaMaster.Domain.Common;
using PizzaMaster.Domain.Konten;
using Xunit;
using Xunit.Abstractions;

namespace PizzaMaster.Tests
{
    [Collection("db")]
    public abstract class DomainTestBase : IDisposable
    {
        protected static readonly Benutzer Benni = new Benutzer("Benni");
        protected static readonly Benutzer Frank = new Benutzer("Frank");

        protected readonly PizzaMasterClient Client;

        protected DomainTestBase(DatabaseFixture dbFixture, ITestOutputHelper output)
        {
            var dataFileName = dbFixture.GetRandomDataFileName();

            this.Client = PizzaMasterApplication.Create()
                                                .ConfigureLiteDb(dataFileName)
                                                .Run();
        }

        public void Dispose()
        {
            this.Client.Dispose();
        }

        protected static Transaktion Abbuchung(decimal betrag, decimal saldo, string beschreibung)
        {
            return new Transaktion(EventId.New, DateTimeOffset.Now, Transaktionstyp.Abbuchung, betrag, saldo,
                                   beschreibung);
        }

        protected static Transaktion Einzahlung(decimal betrag, decimal saldo)
        {
            return new Transaktion(EventId.New, DateTimeOffset.Now, Transaktionstyp.Einzahlung, betrag, saldo);
        }
    }
}