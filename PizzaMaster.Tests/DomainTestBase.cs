using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PizzaMaster.Application;
using PizzaMaster.Application.Client;
using PizzaMaster.Domain.Common;
using PizzaMaster.Domain.Konten;
using PizzaMaster.Query.Konten;
using Xunit.Abstractions;

namespace PizzaMaster.Tests
{
    public abstract class DomainTestBase : IDisposable
    {
        protected static readonly Benutzer Benni = new Benutzer("Benni");
        protected static readonly Benutzer Frank = new Benutzer("Frank");

        protected readonly PizzaMasterClient Client;
        private readonly MemoryStream db;

        protected DomainTestBase(ITestOutputHelper output)
        {
            this.db = new MemoryStream();
            this.Client = PizzaMasterApplication.Create()
                                                .ConfigureLiteDb(this.db)
                                                .Run();
        }

        public void Dispose()
        {
            this.Client.Dispose();
            this.db.Dispose();
        }

        protected static Transaktion Abbuchung(decimal betrag, decimal saldo, string beschreibung)
        {
            return new Transaktion(DateTimeOffset.Now, Transaktionstyp.Abbuchung, betrag, saldo,
                                   beschreibung);
        }

        protected static Transaktion Einzahlung(
            decimal betrag,
            decimal saldo,
            Einzahlungsart einzahlungsart = Einzahlungsart.Bareinzahlung)
        {
            return new Transaktion(DateTimeOffset.Now, Transaktionstyp.Einzahlung, betrag, saldo,
                                   einzahlungsart.ToString());
        }
    }
}