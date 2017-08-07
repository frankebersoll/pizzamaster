using EventFlow.Aggregates;

namespace PizzaMaster.Bestellungen.Events
{
    public class BestellungBegonnenEvent : AggregateEvent<BestellungAggregate, BestellungId> {
        public string Lieferdienst { get; }

        public BestellungBegonnenEvent(string lieferdienst)
        {
            this.Lieferdienst = lieferdienst;
        }
    }
}