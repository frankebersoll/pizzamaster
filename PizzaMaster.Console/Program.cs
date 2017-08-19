using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaster.Application;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = PizzaMasterApplication.Create().ConfigureLiteDb().Run())
            {
                var frank = new Benutzer("Frank");

                var konto = client.TryGetKonto(frank) ?? client.KontoEroeffnen(frank);
                konto.Einzahlen(20);
                konto.Abbuchen(5, "Blah");
                System.Console.ReadLine();
            }
        }
    }
}