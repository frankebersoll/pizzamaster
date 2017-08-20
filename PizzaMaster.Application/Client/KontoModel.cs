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
    public class KontoModel : ClientModelBase<KontoReadModel, KontoId>
    {
        private Transaktion[] transaktionen;

        public KontoModel(KontoId id, PizzaMasterClient client) : base(client, id)
        {
        }

        public KontoModel(KontoReadModel entity, PizzaMasterClient client) : base(client, new KontoId(entity.Id), entity)
        {
        }

        public Benutzer Benutzer => new Benutzer(this.ReadModel.Benutzer);

        public decimal Saldo => this.ReadModel.Saldo;

        public IEnumerable<Transaktion> Transaktionen => this.transaktionen ?? this.LoadTransaktionen();

        public void Abbuchen(Betrag betrag, string beschreibung)
        {
            this.Publish(new AbbuchenCommand(this.Id, betrag, beschreibung));
        }

        public void Aufloesen()
        {
            this.Publish(new KontoAufloesenCommand(this.Id));
        }

        public void Einzahlen(Betrag betrag)
        {
            this.Publish(new EinzahlenCommand(this.Id, betrag));
        }

        private Transaktion[] LoadTransaktionen()
        {
            return this.transaktionen = this.Query(new TransaktionenQuery(this.Id)).ToArray();
        }

        public override void Refresh()
        {
            base.Refresh();
            this.transaktionen = null;
        }

        protected override KontoReadModel QueryReadModel()
        {
            return this.Query(new ReadModelByIdQuery<KontoReadModel>(this.Id));
        }
    }
}