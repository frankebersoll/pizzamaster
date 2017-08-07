using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using PizzaMaster.Konten.ReadModels;

namespace PizzaMaster.Konten.Queries {
    public class GetAllKontenQueryHandler : IQueryHandler<GetAllKontenQuery, IEnumerable<Konto>>
    {
        private readonly IInMemoryReadStore<KontoReadModel> store;

        public GetAllKontenQueryHandler(IInMemoryReadStore<KontoReadModel> store)
        {
            this.store = store;
        }

        public async Task<IEnumerable<Konto>> ExecuteQueryAsync(GetAllKontenQuery query, CancellationToken cancellationToken)
        {
            var models = await this.store.FindAsync(m => !m.IsAufgeloest, CancellationToken.None);
            return models.Select(m => m.ToEntity());
        }
    }
}