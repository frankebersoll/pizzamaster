using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Queries;
using PizzaMaster.Domain.Common;
using PizzaMaster.Domain.Konten;
using PizzaMaster.Domain.Konten.Commands;
using PizzaMaster.Query.Konten;

namespace PizzaMaster.Application.Client
{
    public class KontoModel : ClientModelBase
    {
        private Konto entity;
        private Transaktion[] transaktionen;

        public KontoModel(KontoId id, PizzaMasterClient client) : base(client)
        {
            this.Id = id;
        }

        public KontoModel(Konto entity, PizzaMasterClient client) : base(client)
        {
            this.Id = entity.Id;
            this.entity = entity;
        }

        public Benutzer Benutzer => this.Model.Benutzer;

        public KontoId Id { get; }

        private Konto Model => this.entity ?? this.LoadEntity();

        public decimal Saldo => this.Model.Saldo;

        public IEnumerable<Transaktion> Transaktionen => this.transaktionen ?? this.LoadTransaktionen();

        public void Abbuchen(decimal betrag, string beschreibung)
        {
            this.Client.Publish(new AbbuchenCommand(this.Id, betrag, beschreibung));
        }

        public void Aufloesen()
        {
            this.Client.Publish(new KontoAufloesenCommand(this.Id));
        }

        public void Einzahlen(decimal betrag)
        {
            this.Client.Publish(new EinzahlenCommand(this.Id, betrag));
        }

        private Konto LoadEntity()
        {
            return this.entity = this.Client.Query(new ReadModelByIdQuery<KontoReadModel>(this.Id)).ToEntity();
        }

        private Transaktion[] LoadTransaktionen()
        {
            return this.transaktionen = this.Client.Query(new TransaktionenQuery(this.Id)).ToArray();
        }
    }
}