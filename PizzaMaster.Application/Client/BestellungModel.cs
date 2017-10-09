using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Queries;
using PizzaMaster.Domain.Bestellungen;
using PizzaMaster.Domain.Bestellungen.Commands;
using PizzaMaster.Domain.Bestellungen.Entities;
using PizzaMaster.Domain.Common;
using PizzaMaster.Query.Bestellungen;

namespace PizzaMaster.Application.Client
{
    public class BestellungModel : ClientModelBase<BestellungReadModel, BestellungId>
    {
        public BestellungModel(BestellungId id, PizzaMasterClient client) : base(client, id) { }

        public BestellungModel(BestellungReadModel readModel, PizzaMasterClient client) :
            base(client, new BestellungId(readModel.Id), readModel) { }

        public ArtikelModel[] Artikel => this.ReadModel.Artikel.Select(a => new ArtikelModel(a, this)).ToArray();

        public DateTime Datum => this.ReadModel.Datum;

        public bool IstAbgeschlossen => this.ReadModel.IstAbgeschlossen;

        public string Lieferdienst => this.ReadModel.Lieferdienst;

        public void Abbrechen()
        {
            this.Publish(new BestellungAbbrechenCommand(this.Id));
        }

        public void Abschliessen()
        {
            this.Publish(new BestellungAbschliessenCommand(this.Id));
        }

        internal void ArtikelEntfernen(ArtikelId artikelId)
        {
            this.Publish(new ArtikelEntfernenCommand(this.Id, artikelId));
        }

        public BestellungModel ArtikelHinzufuegen(Betrag betrag, string beschreibung, Benutzer benutzer = null)
        {
            this.Publish(new ArtikelHinzufuegenCommand(this.Id, betrag, beschreibung, benutzer));
            return this;
        }

        internal void ArtikelZuordnen(ArtikelId artikelId, Benutzer benutzer)
        {
            this.Publish(new ArtikelZuordnenCommand(this.Id, artikelId, benutzer));
        }

        protected override BestellungReadModel QueryReadModel()
        {
            return this.Query(new ReadModelByIdQuery<BestellungReadModel>(this.Id));
        }
    }
}