using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Entities;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Konten
{
    public class Konto : Entity<KontoId>
    {
        public Konto(KontoId id, Benutzer benutzer, decimal saldo) : base(id)
        {
            this.Saldo = saldo;
            this.Benutzer = benutzer;
        }

        public Benutzer Benutzer { get; }

        public decimal Saldo { get; }
    }
}