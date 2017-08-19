using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using PizzaMaster.Domain.Konten;

namespace PizzaMaster.Query.Konten
{
    class GetAllKontenQueryHandler : IQueryHandler<GetAllKontenQuery, IEnumerable<Konto>>
    {
        private readonly ISearchableReadModelStore<KontoReadModel> store;

        public GetAllKontenQueryHandler(ISearchableReadModelStore<KontoReadModel> store)
        {
            this.store = store;
        }

        public async Task<IEnumerable<Konto>> ExecuteQueryAsync(
            GetAllKontenQuery query,
            CancellationToken cancellationToken)
        {
            var models = await this.store.FindAsync(m => !m.IsAufgeloest, cancellationToken);
            return models.Select(m => m.ToEntity());
        }
    }
}