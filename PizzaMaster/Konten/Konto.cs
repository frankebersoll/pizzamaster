using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Entities;
using PizzaMaster.Konten.ValueObjects;

namespace PizzaMaster.Konten {
    public class Konto : Entity<KontoId>
    {
        public decimal Saldo { get; }
        
        public Benutzer Benutzer { get; }
        
        public Konto(KontoId id, Benutzer benutzer, decimal saldo) : base(id)
        {
            this.Saldo = saldo;
            this.Benutzer = benutzer;
        }
    }
}