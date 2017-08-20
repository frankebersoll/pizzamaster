using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.Extensions;
using PizzaMaster.Domain.Common;
using PizzaMaster.Domain.Konten.Events;

namespace PizzaMaster.Domain.Konten
{
    public class KontoAggregate : AggregateRoot<KontoAggregate, KontoId>
    {
        private readonly KontoState state = new KontoState();

        public KontoAggregate(KontoId id) : base(id)
        {
            this.Register(this.state);
        }

        public bool IstAufgeloest => this.state.IstAufgeloest;

        public decimal Saldo => this.state.Saldo;

        public void Abbuchen(Betrag betrag, string beschreibung)
        {
            KontoSpecs.NichtAufgeloest
                      .And(Specs.Existiert)
                      .ThrowDomainErrorIfNotStatisfied(this);

            var neuerSaldo = this.Saldo - betrag;
            this.Emit(new Abgebucht(betrag, neuerSaldo, beschreibung));
        }

        public void Aufloesen()
        {
            KontoSpecs.Ausgeglichen
                      .And(KontoSpecs.NichtAufgeloest)
                      .And(Specs.Existiert)
                      .ThrowDomainErrorIfNotStatisfied(this);

            this.Emit(new KontoAufgeloest());
        }

        public void Einzahlen(Betrag betrag)
        {
            KontoSpecs.NichtAufgeloest
                      .And(Specs.Existiert)
                      .ThrowDomainErrorIfNotStatisfied(this);

            var neuerSaldo = this.Saldo + betrag;
            this.Emit(new Eingezahlt(betrag, neuerSaldo));
        }

        public void Eroeffnen(Benutzer benutzer)
        {
            Specs.IstNeu.ThrowDomainErrorIfNotStatisfied(this);

            this.Emit(new KontoEroeffnet(benutzer));
        }
    }
}