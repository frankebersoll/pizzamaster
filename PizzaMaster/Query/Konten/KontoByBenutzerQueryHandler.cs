using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;

namespace PizzaMaster.Query.Konten
{
    class KontoByBenutzerQueryHandler : IQueryHandler<KontoByBenutzerQuery, KontoReadModel>
    {
        private readonly ISearchableReadModelStore<KontoReadModel> store;

        public KontoByBenutzerQueryHandler(ISearchableReadModelStore<KontoReadModel> store)
        {
            this.store = store;
        }

        public async Task<KontoReadModel> ExecuteQueryAsync(
            KontoByBenutzerQuery query,
            CancellationToken cancellationToken)
        {
            var benutzer = query.Benutzer;
            var models = (await this.store.FindAsync(m => m.Benutzer == benutzer && !m.IsAufgeloest, cancellationToken))
                .ToArray();
            switch (models.Length)
            {
                case 0:
                    if (query.ThrowIfNotFound)
                        throw new Exception("Not found.");
                    else return null;
                case 1:
                    return models.Single();
                default:
                    throw new MehrereKontenMitSelbemBenutzerException();
            }
        }
    }
}