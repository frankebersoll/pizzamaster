using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            return events
                .Where(TransaktionEvent.IsTransaktion)
                .Select(Transaktion.FromEvent);
        }
    }
}