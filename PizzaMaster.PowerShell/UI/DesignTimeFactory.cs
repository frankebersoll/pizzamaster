using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PizzaMaster.Application;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.PowerShell.UI
{
    public static class DesignTimeFactory
    {
        static DesignTimeFactory()
        {
            var client = PizzaMasterApplication
                .Create()
                .ConfigureLiteDb(new MemoryStream())
                .Run();

            var benutzer = new[]
                           {
                               new Benutzer("benni"),
                               new Benutzer("frank")
                           };

            var bestellung = client.BestellungBeginnen("Pizzeria Bella Mia")
                                   .ArtikelHinzufuegen(7.5m, "Quattro Formaggio")
                                   .ArtikelHinzufuegen(6.9m, "Tortellini alla Panna")
                                   .ArtikelHinzufuegen(3, "Pizzabrötchen");

            Zuordnen = new ZuordnenViewModel(bestellung.Artikel, benutzer);
        }

        public static ZuordnenViewModel Zuordnen { get; }
    }
}