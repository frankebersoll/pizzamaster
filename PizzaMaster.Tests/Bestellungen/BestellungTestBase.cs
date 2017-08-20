using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaster.Application.Client;
using PizzaMaster.Domain.Common;
using Xunit.Abstractions;

namespace PizzaMaster.Tests.Bestellungen
{
    public abstract class BestellungTestBase : DomainTestBase
    {
        protected readonly BestellungModel Bestellung;

        protected BestellungTestBase(ITestOutputHelper output) : base(output)
        {
            this.Bestellung = this.CreateBestellung();
        }

        protected virtual BestellungModel CreateBestellung()
        {
            return this.Client.BestellungBeginnen("Eurasia");
        }

        protected KontoModel GetKonto(Benutzer benutzer)
        {
            return this.Client.TryGetKonto(benutzer);
        }
    }
}