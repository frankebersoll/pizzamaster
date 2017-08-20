using EventFlow.Aggregates;

namespace PizzaMaster.Domain.Bestellungen.Events {
    public class BestellungAbgebrochen : AggregateEvent<BestellungAggregate, BestellungId> { }
}