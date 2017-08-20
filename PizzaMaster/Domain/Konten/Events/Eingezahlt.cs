using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Konten.Events
{
    public class Eingezahlt : TransaktionEvent
    {
        public Eingezahlt(Betrag betrag, decimal saldo) : base(betrag, saldo) { }
    }
}