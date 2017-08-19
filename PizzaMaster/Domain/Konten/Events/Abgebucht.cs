using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaMaster.Domain.Konten.Events
{
    public class Abgebucht : TransaktionEvent
    {
        public Abgebucht(decimal betrag, decimal saldo, string beschreibung) : base(betrag, saldo)
        {
            this.Beschreibung = beschreibung;
        }

        public string Beschreibung { get; }
    }
}