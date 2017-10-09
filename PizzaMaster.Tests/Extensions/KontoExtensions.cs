using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Equivalency;
using PizzaMaster.Application.Client;
using PizzaMaster.Query.Konten;

namespace PizzaMaster.Tests.Extensions
{
    public static class KontoExtensions
    {
        private static EquivalencyAssertionOptions<Transaktion> CompareTransaktion(
            EquivalencyAssertionOptions<Transaktion> o)
        {
            return o.Excluding(t => t.Timestamp);
        }

        public static void TransaktionenShouldBe(this KontoModel konto, params Transaktion[] transaktionen)
        {
            konto.LetzteTransaktion.ShouldBeEquivalentTo(transaktionen.Last(), CompareTransaktion);

            konto.Transaktionen.ShouldAllBeEquivalentTo(transaktionen, CompareTransaktion);
        }
    }
}