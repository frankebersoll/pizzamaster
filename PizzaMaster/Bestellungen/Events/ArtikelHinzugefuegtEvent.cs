using EventFlow.Aggregates;
using PizzaMaster.Konten;
using PizzaMaster.Konten.ValueObjects;

namespace PizzaMaster.Bestellungen.Events
{
    public class ArtikelHinzugefuegtEvent : AggregateEvent<BestellungAggregate, BestellungId>
    {
        public ArtikelId Id { get; }

        public Benutzer Benutzer { get; }

        public decimal Betrag { get; }

        public string Beschreibung { get; }

        public ArtikelHinzugefuegtEvent(ArtikelId id, Benutzer benutzer, decimal betrag, string beschreibung)
        {
            this.Id = id;
            this.Benutzer = benutzer;
            this.Betrag = betrag;
            this.Beschreibung = beschreibung;
        }
    }
}