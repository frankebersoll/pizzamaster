using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Extensions;
using EventFlow.Logs;
using EventFlow.ReadStores;
using LiteDB;
using PizzaMaster.Query;

namespace PizzaMaster.Application.LiteDb
{
    public class LiteDbReadModelStore<TReadModel> : ISearchableReadModelStore<TReadModel>
        where TReadModel : class, IVersionedReadModel, new()
    {
        private readonly LiteDatabase liteDatabase;
        private readonly ILog log;
        private readonly IReadModelDescriptionProvider readModelDescriptionProvider;

        public LiteDbReadModelStore(
            ILog log,
            LiteDatabase liteDatabase,
            IReadModelDescriptionProvider readModelDescriptionProvider)
        {
            this.log = log;
            this.liteDatabase = liteDatabase;
            this.readModelDescriptionProvider = readModelDescriptionProvider;
        }

        public Task DeleteAllAsync(CancellationToken cancellationToken)
        {
            var readModelDescription = this.readModelDescriptionProvider.GetReadModelDescription<TReadModel>();

            this.log.Information($"Deleting ALL '{typeof(TReadModel).PrettyPrint()}' by DROPPING COLLECTION '{readModelDescription.RootCollectionName}'!");

            this.liteDatabase.DropCollection(readModelDescription.RootCollectionName.Value);

            return Task.FromResult(0);
        }

        public Task<ReadModelEnvelope<TReadModel>> GetAsync(string id, CancellationToken cancellationToken)
        {
            var readModelDescription = this.readModelDescriptionProvider.GetReadModelDescription<TReadModel>();

            this.log.Verbose(() =>
                                 $"Fetching read model '{typeof(TReadModel).PrettyPrint()}' with _id '{id}' from collection '{readModelDescription.RootCollectionName}'");

            var collection = this.liteDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);
            var result = collection.FindById(new BsonValue(id));
            return Task.FromResult(ReadModelEnvelope<TReadModel>.With(id, result));
        }

        public async Task UpdateAsync(
            IReadOnlyCollection<ReadModelUpdate> readModelUpdates,
            IReadModelContext readModelContext,
            Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken,
                Task<ReadModelEnvelope<TReadModel>>> updateReadModel,
            CancellationToken cancellationToken)
        {
            var readModelDescription = this.readModelDescriptionProvider.GetReadModelDescription<TReadModel>();

            this.log.Verbose(() =>
            {
                var readModelIds = readModelUpdates
                    .Select(u => u.ReadModelId)
                    .Distinct()
                    .OrderBy(i => i)
                    .ToList();
                return
                    $"Updating read models of type '{typeof(TReadModel).PrettyPrint()}' with _ids '{string.Join(", ", readModelIds)}' in collection '{readModelDescription.RootCollectionName}'";
            });

            foreach (var readModelUpdate in readModelUpdates)
            {
                var collection =
                    this.liteDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);
                var result = collection.FindById(new BsonValue(readModelUpdate.ReadModelId));

                var readModelEnvelope = result != null
                                            ? ReadModelEnvelope<TReadModel>.With(readModelUpdate.ReadModelId, result)
                                            : ReadModelEnvelope<TReadModel>.Empty(readModelUpdate.ReadModelId);

                readModelEnvelope =
                    await updateReadModel(readModelContext, readModelUpdate.DomainEvents, readModelEnvelope,
                                          cancellationToken).ConfigureAwait(false);

                readModelEnvelope.ReadModel.Version = readModelEnvelope.Version;

                collection.Upsert(new BsonValue(readModelUpdate.ReadModelId),
                                  readModelEnvelope.ReadModel);
            }
        }

        public Task<IReadOnlyCollection<TReadModel>> FindAsync(
            Expression<Func<TReadModel, bool>> predicate,
            CancellationToken cancellationToken)
        {
            var readModelDescription = this.readModelDescriptionProvider.GetReadModelDescription<TReadModel>();

            var collection = this.liteDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);
            IReadOnlyCollection<TReadModel> result = collection.Find(predicate).ToImmutableArray();
            return Task.FromResult(result);
        }
    }
}