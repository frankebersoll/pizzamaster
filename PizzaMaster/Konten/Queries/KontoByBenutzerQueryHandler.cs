using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using PizzaMaster.Konten.ReadModels;

namespace PizzaMaster.Konten.Queries
{
    public class KontoByBenutzerQueryHandler : IQueryHandler<KontoByBenutzerQuery, Konto>
    {
        private readonly IInMemoryReadStore<KontoReadModel> store;

        public KontoByBenutzerQueryHandler(IInMemoryReadStore<KontoReadModel> store)
        {
            this.store = store;
        }

        public async Task<Konto> ExecuteQueryAsync(KontoByBenutzerQuery query, CancellationToken cancellationToken)
        {
            var models = await this.store.FindAsync(m => m.Benutzer == query.Benutzer && !m.IsAufgeloest, cancellationToken);
            switch (models.Count)
            {
                case 0:
                    throw new Exception("Not found.");
                case 1:
                    return models.Single().ToEntity();
                default:
                    throw new MehrereKontenMitSelbemBenutzerException();
            }
        }
    }
}