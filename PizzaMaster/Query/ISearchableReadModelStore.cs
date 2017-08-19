using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.ReadStores;

namespace PizzaMaster.Query
{
    public interface ISearchableReadModelStore<TReadModel> : IReadModelStore<TReadModel>
        where TReadModel : class, IReadModel, new()
    {
        Task<IReadOnlyCollection<TReadModel>> FindAsync(
            Expression<Func<TReadModel, bool>> predicate,
            CancellationToken cancellationToken);
    }
}