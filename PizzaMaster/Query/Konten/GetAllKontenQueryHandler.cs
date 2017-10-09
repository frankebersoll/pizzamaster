using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;

namespace PizzaMaster.Query.Konten
{
    class GetAllKontenQueryHandler : IQueryHandler<GetAllKontenQuery, IEnumerable<KontoReadModel>>
    {
        private readonly ISearchableReadModelStore<KontoReadModel> store;

        public GetAllKontenQueryHandler(ISearchableReadModelStore<KontoReadModel> store)
        {
            this.store = store;
        }

        public async Task<IEnumerable<KontoReadModel>> ExecuteQueryAsync(
            GetAllKontenQuery query,
            CancellationToken cancellationToken)
        {
            var models = await this.store.FindAsync(m => !m.IsAufgeloest, cancellationToken);
            return models;
        }
    }
}