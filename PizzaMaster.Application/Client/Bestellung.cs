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
    public class Bestellung : ClientModelBase
    {
        public Bestellung(BestellungId id, PizzaMasterClient client) : base(client)
        {
            this.Id = id;
        }

        public BestellungId Id { get; }

        public BestellungReadModel Model =>
            this.Client.Query(new ReadModelByIdQuery<BestellungReadModel>(this.Id));

        public void Abschliessen()
        {
            this.Client.Publish(new BestellungAbschliessenCommand(this.Id));
        }

        public Bestellung ArtikelHinzufuegen(decimal betrag, string beschreibung, Benutzer benutzer = null)
        {
            this.Client.Publish(new ArtikelHinzufuegenCommand(this.Id, betrag, beschreibung, benutzer));
            return this;
        }

        public void ArtikelZuordnen(ArtikelId artikelId, Benutzer benutzer)
        {
            this.Client.Publish(new ArtikelZuordnenCommand(this.Id, artikelId, benutzer));
        }
    }
}