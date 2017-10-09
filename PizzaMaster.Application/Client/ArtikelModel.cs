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
        private readonly ArtikelReadModel readModel;

        public ArtikelModel(ArtikelReadModel readModel, BestellungModel bestellung)
        {
            this.readModel = readModel;
            this.Bestellung = bestellung;
        }

        public Benutzer Benutzer => this.readModel.Benutzer;

        public string Beschreibung => this.readModel.Beschreibung;

        public BestellungModel Bestellung { get; }

        public Betrag Betrag => this.readModel.Betrag;

        public void Entfernen()
        {
            this.Bestellung.ArtikelEntfernen(new ArtikelId(this.readModel.Id));
        }

        public void Zuordnen(Benutzer benutzer)
        {
            this.Bestellung.ArtikelZuordnen(new ArtikelId(this.readModel.Id), benutzer);
        }
    }
}