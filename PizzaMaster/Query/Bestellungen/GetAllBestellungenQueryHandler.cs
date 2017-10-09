using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;

namespace PizzaMaster.Query.Bestellungen
{
    public class GetAllBestellungenQueryHandler
        : IQueryHandler<GetAllBestellungenQuery, IEnumerable<BestellungReadModel>>
    {
        private readonly ISearchableReadModelStore<BestellungReadModel> store;

        public GetAllBestellungenQueryHandler(ISearchableReadModelStore<BestellungReadModel> store)
        {
            this.store = store;
        }

        public async Task<IEnumerable<BestellungReadModel>> ExecuteQueryAsync(
            GetAllBestellungenQuery query,
            CancellationToken cancellationToken)
        {
            var result = await this.store.FindAsync(b => !b.IstAbgeschlossen, cancellationToken);
            return result;
        }
    }
}