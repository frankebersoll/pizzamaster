using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.EventStores;
using EventFlow.Queries;
using PizzaMaster.Domain.Konten;
using PizzaMaster.Domain.Konten.Events;

namespace PizzaMaster.Query.Konten
{
    public class TransaktionenQueryHandler : IQueryHandler<TransaktionenQuery, IEnumerable<Transaktion>>
    {
        private readonly IEventStore eventStore;

        public TransaktionenQueryHandler(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public async Task<IEnumerable<Transaktion>> ExecuteQueryAsync(
            TransaktionenQuery query,
            CancellationToken cancellationToken)
        {
            var events = await this.eventStore.LoadEventsAsync<KontoAggregate, KontoId>(query.Konto, cancellationToken);
            return this.CreateTransaktionen(events);
        }

        private IEnumerable<Transaktion> CreateTransaktionen(IEnumerable<IDomainEvent<KontoAggregate, KontoId>> events)
        {
            foreach (var domainEvent in events)
            {
                var aggregateEvent = domainEvent.GetAggregateEvent();
                var eventId = domainEvent.Metadata.EventId;
                var timestamp = domainEvent.Timestamp;

                switch (aggregateEvent)
                {
                    case Eingezahlt eingezahlt:
                        yield return new Transaktion(eventId, timestamp, eingezahlt);
                        break;
                    case Abgebucht abgebucht:
                        yield return new Transaktion(eventId, timestamp, abgebucht);
                        break;
                }
            }
        }
    }
}