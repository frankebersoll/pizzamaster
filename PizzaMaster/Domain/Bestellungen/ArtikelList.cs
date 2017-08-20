using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PizzaMaster.Domain.Bestellungen.Entities;

namespace PizzaMaster.Domain.Bestellungen
{
    internal class ArtikelList : KeyedCollection<ArtikelId, Artikel>
    {
        public ArtikelList(IReadOnlyList<Artikel> value)
        {
            foreach (var artikel in value)
            {
                this.Add(artikel);
            }
        }

        public ArtikelList() { }

        protected override ArtikelId GetKeyForItem(Artikel item)
        {
            return item.Id;
        }
    }
}