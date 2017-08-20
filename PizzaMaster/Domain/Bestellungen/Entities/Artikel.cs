using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Entities;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Bestellungen.Entities
{
    public class Artikel : Entity<ArtikelId>
    {
        public Artikel(ArtikelId id, Betrag betrag, string beschreibung, Benutzer benutzer = null) : base(id)
        {
            this.Betrag = betrag;
            this.Beschreibung = beschreibung;
            this.Benutzer = benutzer;
        }

        public Benutzer Benutzer { get; set; }

        public string Beschreibung { get; }

        public Betrag Betrag { get; }

        public bool IstZugeordnet => this.Benutzer != null;

        public ArtikelStatus Status { get; set; }
    }
}