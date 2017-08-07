using EventFlow.Commands;
using EventFlow.Core;
using PizzaMaster.Konten.ValueObjects;

namespace PizzaMaster.Bestellungen.Commands
{
    public class ArtikelHinzufuegenCommand : Command<BestellungAggregate, BestellungId>
    {
        public Benutzer Benutzer { get; }

        public decimal Betrag { get; }

        public string Beschreibung { get; }

        public ArtikelHinzufuegenCommand(BestellungId aggregateId, Benutzer benutzer, decimal betrag, string beschreibung) 
            : base(aggregateId)
        {
            this.Benutzer = benutzer;
            this.Betrag = betrag;
            this.Beschreibung = beschreibung;
        }
    }
}