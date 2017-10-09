using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaMaster.PowerShell
{
    public class Rechnung : IComparable
    {
        public decimal Betrag { get; set; }

        public static Comparer<Rechnung> Comparer { get; } = new DatumRelationalComparer();

        public DateTime Datum { get; set; }

        public string Lieferdienst { get; set; }

        public int CompareTo(object obj)
        {
            return Comparer.Compare(this, obj as Rechnung);
        }

        private sealed class DatumRelationalComparer : Comparer<Rechnung>
        {
            public override int Compare(Rechnung x, Rechnung y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(null, y)) return 1;
                if (ReferenceEquals(null, x)) return -1;
                return x.Datum.CompareTo(y.Datum);
            }
        }
    }
}