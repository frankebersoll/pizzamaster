using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaster.Domain.Bestellungen.Entities;

namespace PizzaMaster.Query.Bestellungen
{
    public class ArtikelReadModel
    {
        public ArtikelReadModel() { }

        public ArtikelReadModel(Artikel artikel)
        {
            this.Id = artikel.Id.Value;
            this.Beschreibung = artikel.Beschreibung;
            this.Betrag = artikel.Betrag;
            this.Benutzer = artikel.Benutzer?.Value;
        }

        public string Benutzer { get; set; }

        public string Beschreibung { get; private set; }

        public decimal Betrag { get; private set; }

        public string Id { get; private set; }
    }
}