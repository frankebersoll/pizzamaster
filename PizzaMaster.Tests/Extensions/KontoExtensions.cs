using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PizzaMaster.Application.Client;
using PizzaMaster.Domain.Konten;

namespace PizzaMaster.Tests.Extensions
{
    public static class KontoExtensions
    {
        public static void TransaktionenShouldBe(this KontoModel konto, params Transaktion[] transaktionen)
        {
            konto.Transaktionen
                 .ShouldAllBeEquivalentTo(transaktionen,
                                          o => o.Including(t => t.Beschreibung)
                                                .Including(t => t.Betrag)
                                                .Including(t => t.Saldo)
                                                .Including(t => t.Typ));
        }
    }
}