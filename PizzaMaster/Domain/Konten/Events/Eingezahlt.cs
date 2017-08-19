using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaMaster.Domain.Konten.Events
{
    public class Eingezahlt : TransaktionEvent
    {
        public Eingezahlt(decimal betrag, decimal saldo) : base(betrag, saldo) { }
    }
}