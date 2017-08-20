using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaster.Domain.Bestellungen.Entities;
using PizzaMaster.Domain.Common;
using PizzaMaster.Query.Bestellungen;

namespace PizzaMaster.Application.Client
{
    public class ArtikelModel
    {
        private readonly BestellungModel bestellung;
        private readonly string id;

        public ArtikelModel(ArtikelReadModel readModel, BestellungModel bestellung)
        {
            this.id = readModel.Id;
            this.bestellung = bestellung;
            this.Benutzer = readModel.Benutzer;
            this.Betrag = readModel.Betrag;
        }

        public string Benutzer { get; }

        public decimal Betrag { get; }

        public void Entfernen()
        {
            this.bestellung.ArtikelEntfernen(new ArtikelId(this.id));
        }

        public void Zuordnen(Benutzer benutzer)
        {
            this.bestellung.ArtikelZuordnen(new ArtikelId(this.id), benutzer);
        }
    }
}