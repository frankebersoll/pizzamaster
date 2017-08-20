using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.Entities;
using PizzaMaster.Domain.Common;
using PizzaMaster.Domain.Konten.Events;

namespace PizzaMaster.Domain.Konten
{
    public class Transaktion : Entity<IEventId>
    {
        public Transaktion(
            IEventId id,
            DateTimeOffset timestamp,
            Transaktionstyp typ,
            Betrag betrag,
            decimal saldo,
            string beschreibung = null) : base(id)
        {
            this.Timestamp = timestamp;
            this.Betrag = betrag;
            this.Saldo = saldo;
            this.Beschreibung = beschreibung;
            this.Typ = typ;
        }

        public Transaktion(IEventId id, DateTimeOffset timestamp, Eingezahlt eingezahlt)
            : this(id, timestamp, Transaktionstyp.Einzahlung, eingezahlt.Betrag, eingezahlt.Saldo) { }

        public Transaktion(IEventId id, DateTimeOffset timestamp, Abgebucht abgebucht)
            : this(id, timestamp, Transaktionstyp.Abbuchung, abgebucht.Betrag, abgebucht.Saldo,
                   abgebucht.Beschreibung) { }

        public string Beschreibung { get; }

        public Betrag Betrag { get; }

        public decimal Saldo { get; }

        public DateTimeOffset Timestamp { get; }

        public Transaktionstyp Typ { get; }
    }
}