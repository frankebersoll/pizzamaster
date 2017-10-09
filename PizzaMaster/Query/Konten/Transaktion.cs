using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Domain.Common;
using PizzaMaster.Domain.Konten.Events;

namespace PizzaMaster.Query.Konten
{
    public class Transaktion
    {
        public Transaktion() { }

        public Transaktion(
            DateTimeOffset timestamp,
            Transaktionstyp typ,
            Betrag betrag,
            decimal saldo,
            string beschreibung)
        {
            this.Timestamp = timestamp;
            this.Betrag = betrag;
            this.Saldo = saldo;
            this.Beschreibung = beschreibung;
            this.Typ = typ;
        }

        public Transaktion(DateTimeOffset timestamp, Eingezahlt eingezahlt)
            : this(timestamp, Transaktionstyp.Einzahlung, eingezahlt.Betrag, eingezahlt.Saldo,
                   eingezahlt.Einzahlungsart.ToString()) { }

        public Transaktion(DateTimeOffset timestamp, Abgebucht abgebucht)
            : this(timestamp, Transaktionstyp.Abbuchung, abgebucht.Betrag, abgebucht.Saldo,
                   abgebucht.Beschreibung) { }

        public string Beschreibung { get; private set; }

        public Betrag Betrag { get; private set; }

        public decimal Saldo { get; private set; }

        public DateTimeOffset Timestamp { get; private set; }

        public Transaktionstyp Typ { get; private set; }

        public decimal Wert => this.Typ == Transaktionstyp.Einzahlung
                                   ? this.Betrag.Value
                                   : -this.Betrag.Value;

        public static Transaktion FromEvent(IDomainEvent domainEvent)
        {
            var aggregateEvent = domainEvent.GetAggregateEvent();
            var timestamp = domainEvent.Timestamp;

            switch (aggregateEvent)
            {
                case Eingezahlt eingezahlt:
                    return new Transaktion(timestamp, eingezahlt);
                case Abgebucht abgebucht:
                    return new Transaktion(timestamp, abgebucht);
            }

            throw new ArgumentException(nameof(domainEvent));
        }
    }
}