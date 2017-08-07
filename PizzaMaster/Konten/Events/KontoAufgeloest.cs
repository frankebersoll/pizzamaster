using EventFlow.Aggregates;

namespace PizzaMaster.Konten.Events
{
    public class KontoAufgeloest : AggregateEvent<KontoAggregate, KontoId>
    {
        public KontoAufgeloest() { }
    }
}