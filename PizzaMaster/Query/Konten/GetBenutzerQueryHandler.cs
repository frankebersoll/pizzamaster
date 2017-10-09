using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Query.Konten
{
    public class GetBenutzerQueryHandler : IQueryHandler<GetBenutzerQuery, Benutzer[]>
    {
        private readonly ISearchableReadModelStore<BenutzerReadModel> store;

        public GetBenutzerQueryHandler(ISearchableReadModelStore<BenutzerReadModel> store)
        {
            this.store = store;
        }

        public async Task<Benutzer[]> ExecuteQueryAsync(GetBenutzerQuery query, CancellationToken cancellationToken)
        {
            var readModel = await this.store.GetAsync("Benutzer", cancellationToken);
            return readModel.ReadModel?.Benutzer ?? new Benutzer[0];
        }
    }
}