using EventFlow.Aggregates;
using PizzaMaster.Bestellungen;

namespace PizzaMaster.Konten.Events
{
    public class AbgebuchtEvent : AggregateEvent<KontoAggregate, KontoId>
    {
        public decimal Betrag { get; }
        
        public string Beschreibung { get; }
        
        public ArtikelId Artikel { get; }
    }
}