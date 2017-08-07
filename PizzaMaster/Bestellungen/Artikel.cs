using EventFlow.Entities;
using PizzaMaster.Bestellungen.Events;
using PizzaMaster.Konten.ValueObjects;

namespace PizzaMaster.Bestellungen
{
    public class Artikel : Entity<ArtikelId>
    {
        public Benutzer Benutzer { get; }

        public decimal Betrag { get; }
        
        public string Beschreibung { get; }

        public bool IsBeglichen => this.Betrag == 0;

        public Artikel(ArtikelId id, Benutzer benutzer, decimal betrag, string beschreibung) : base(id)
        {
            this.Benutzer = benutzer;
            this.Betrag = betrag;
            this.Beschreibung = beschreibung;
        }

        public static Artikel FromEvent(ArtikelHinzugefuegtEvent e)
        {
            return new Artikel(e.Id, e.Benutzer, e.Betrag, e.Beschreibung);
        }
    }
}