using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.Specifications;
using PizzaMaster.Bestellungen.Events;
using PizzaMaster.Konten;
using PizzaMaster.Konten.ValueObjects;

namespace PizzaMaster.Bestellungen
{
    public class BestellungAggregate : AggregateRoot<BestellungAggregate, BestellungId>,
                                       IEmit<BestellungBegonnenEvent>,
                                       IEmit<ArtikelHinzugefuegtEvent>,
                                       IEmit<BezahlungAngefordertEvent>
    {
        public BestellungAggregate(BestellungId id) : base(id) { }

        public void Beginnen(string lieferdienst)
        {
            this.Emit(new BestellungBegonnenEvent(lieferdienst));
        }

        public void Apply(BestellungBegonnenEvent aggregateEvent)
        {
            this.Lieferdienst = aggregateEvent.Lieferdienst;
        }

        public string Lieferdienst { get; private set; }

        public void ArtikelHinzufuegen(Benutzer benutzer, decimal betrag, string beschreibung)
        {    
            var artikelId = ArtikelId.New;
            this.Emit(new ArtikelHinzugefuegtEvent(artikelId, benutzer, betrag, beschreibung));
        }

        public void Apply(ArtikelHinzugefuegtEvent aggregateEvent)
        {
            this.artikel.Add(Artikel.FromEvent(aggregateEvent));
            this.Betrag += aggregateEvent.Betrag;
        }

        public decimal Betrag { get; private set; }

        private readonly List<Artikel> artikel = new List<Artikel>();

        public void Abschliessen()
        {
            var nichtBeglicheneArtikel = this.artikel.Where(a => !a.IsBeglichen).ToArray();

            foreach (var artikel in nichtBeglicheneArtikel)
            {
                var e = new BezahlungAngefordertEvent(artikel);
                this.Emit(e);
            }
        }

        public void Apply(BezahlungAngefordertEvent aggregateEvent)
        {
        }
    }
}