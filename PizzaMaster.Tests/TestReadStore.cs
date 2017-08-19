using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using EventFlow.ReadStores.InMemory;
using PizzaMaster.Query;

namespace PizzaMaster.Tests
{
    public class TestReadStore<TReadModel> : ISearchableReadModelStore<TReadModel>
        where TReadModel : class, IVersionedReadModel, new()
    {
        private readonly InMemoryReadStore<TReadModel> inner;

        public TestReadStore(InMemoryReadStore<TReadModel> inner)
        {
            this.inner = inner;
        }

        public Task DeleteAllAsync(CancellationToken cancellationToken)
        {
            return this.inner.DeleteAllAsync(cancellationToken);
        }

        public Task<ReadModelEnvelope<TReadModel>> GetAsync(string id, CancellationToken cancellationToken)
        {
            return this.inner.GetAsync(id, cancellationToken);
        }

        public Task UpdateAsync(
            IReadOnlyCollection<ReadModelUpdate> readModelUpdates,
            IReadModelContext readModelContext,
            Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken,
                Task<ReadModelEnvelope<TReadModel>>> updateReadModel,
            CancellationToken cancellationToken)
        {
            return this.inner.UpdateAsync(readModelUpdates, readModelContext, updateReadModel, cancellationToken);
        }

        public Task<IReadOnlyCollection<TReadModel>> FindAsync(
            Expression<Func<TReadModel, bool>> predicate,
            CancellationToken cancellationToken)
        {
            return this.inner.FindAsync(r => predicate.Compile()(r), cancellationToken);
        }
    }
}