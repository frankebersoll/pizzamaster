using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PizzaMaster.Query.Bestellungen
{
    public class ArtikelReadModelCollection : KeyedCollection<string, ArtikelReadModel>
    {
        protected override string GetKeyForItem(ArtikelReadModel item)
        {
            return item.Id;
        }
    }
}