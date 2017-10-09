using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaster.Application;
using PizzaMaster.Domain.Common;
using PizzaMaster.Domain.Konten;

namespace PizzaMaster.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = PizzaMasterApplication.Create().ConfigureLiteDb().Run())
            {
                var k = client.GetOrCreateKonto(new Benutzer("alex"));

                k.Einzahlen(123);
                k.Einzahlen(2, Einzahlungsart.PayPal);

                var konten = client.GetKonten();
                foreach (var transaktion in konten.SelectMany(ko => ko.Transaktionen))
                {
                    System.Console.WriteLine(transaktion.Beschreibung + " " + transaktion.Betrag);
                }
            }

            System.Console.ReadLine();
        }
    }
}