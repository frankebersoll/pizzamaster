using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Konten.Events;
using PizzaMaster.Konten.ValueObjects;

namespace PizzaMaster.Konten {
    public class KontoAggregate : AggregateRoot<KontoAggregate, KontoId>,
                                  IEmit<KontoEroeffnetEvent>,
                                  IEmit<EingezahltEvent>,
                                  IEmit<KontoAufgeloest>
    {
        public KontoAggregate(KontoId id) : base(id) { }

        public void Eroeffnen(Benutzer userName)
        {
            this.CheckAufgeloest();

            this.Emit(new KontoEroeffnetEvent(userName));
        }

        private void CheckAufgeloest()
        {
            if (this.IsAufgeloest)
                throw new InvalidOperationException("Konto wurde aufgelöst.");
        }

        public void Apply(KontoEroeffnetEvent aggregateEvent)
        {
            this.UserName = aggregateEvent;
        }

        public KontoEroeffnetEvent UserName { get; private set; }

        public void Einzahlen(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            this.CheckAufgeloest();

            this.Emit(new EingezahltEvent(amount));
        }

        public void Apply(EingezahltEvent aggregateEvent)
        {
            this.Saldo += aggregateEvent.Amount;
        }

        public decimal Saldo { get; private set; }

        public void Aufloesen()
        {
            if (this.Saldo != 0m)
                throw new InvalidOperationException($"Saldo liegt bei {this.Saldo}, Konto kann nicht aufgelöst werden.");

            this.CheckAufgeloest();

            this.Emit(new KontoAufgeloest());
        }

        public void Apply(KontoAufgeloest aggregateEvent)
        {
            this.IsAufgeloest = true;
        }

        public bool IsAufgeloest { get; private set; }

        public void Abbuchen(decimal betrag, string beschreibung)
        {
            this.Emit(new AbgebuchtEvent());
        }
    }
}