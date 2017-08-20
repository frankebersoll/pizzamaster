using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Konten.Events
{
    public class Abgebucht : TransaktionEvent
    {
        public Abgebucht(Betrag betrag, decimal saldo, string beschreibung) : base(betrag, saldo)
        {
            this.Beschreibung = beschreibung;
        }

        public string Beschreibung { get; }
    }
}