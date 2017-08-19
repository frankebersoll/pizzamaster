using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Core;
using EventFlow.EventStores;
using EventFlow.Exceptions;
using LiteDB;

namespace PizzaMaster.Application.LiteDb
{
    class LiteDbEventPersistance : IEventPersistence
    {
        private readonly LiteCollection<LiteDbEvent> collection;
        private readonly LiteDatabase db;

        public LiteDbEventPersistance(LiteDatabase db, string collectionName)
        {
            if (collectionName == null) throw new ArgumentNullException(nameof(collectionName));

            this.db = db;
            this.collection = db.GetCollection<LiteDbEvent>(collectionName);

            this.collection.EnsureIndex(e => e.Id);
            this.collection.EnsureIndex(e => e.AggregateId);
        }

        public Task<IReadOnlyCollection<ICommittedDomainEvent>> CommitEventsAsync(
            IIdentity id,
            IReadOnlyCollection<SerializedEvent> serializedEvents,
            CancellationToken cancellationToken)
        {
            var committedDomainEvents = serializedEvents
                .Select(e => new LiteDbEvent
                             {
                                 AggregateSequenceNumber = e.AggregateSequenceNumber,
                                 Metadata = e.SerializedMetadata,
                                 AggregateId = id.Value,
                                 Data = e.SerializedData
                             }).ToArray();

            using (var transaction = this.db.BeginTrans())
            {
                var count = this.collection.Count(e => e.AggregateId == id.Value);
                var expectedCount = serializedEvents.First().AggregateSequenceNumber - 1;

                if (count != expectedCount)
                {
                    throw new OptimisticConcurrencyException("Sequence number does already exist.");
                }

                this.collection.Insert(committedDomainEvents);

                transaction.Commit();
            }

            IReadOnlyCollection<ICommittedDomainEvent> domainEvents = committedDomainEvents.ToImmutableArray();
            return Task.FromResult(domainEvents);
        }

        public Task DeleteEventsAsync(IIdentity id, CancellationToken cancellationToken)
        {
            this.collection.Delete(e => e.AggregateId == id.Value);
            return Task.CompletedTask;
        }

        public Task<AllCommittedEventsPage> LoadAllCommittedEvents(
            GlobalPosition globalPosition,
            int pageSize,
            CancellationToken cancellationToken)
        {
            IReadOnlyCollection<LiteDbEvent> result;
            if (globalPosition.IsStart)
            {
                result = this.collection.Find(LiteDB.Query.All(), limit: pageSize).ToImmutableArray();
            }
            else
            {
                var id = new ObjectId(globalPosition.Value);
                result = this.collection.Find(e => e.Id > id, limit: pageSize).ToImmutableArray();
            }

            var position = new GlobalPosition(result.LastOrDefault()?.Id.ToString());
            var page = new AllCommittedEventsPage(position, result);
            return Task.FromResult(page);
        }

        public Task<IReadOnlyCollection<ICommittedDomainEvent>> LoadCommittedEventsAsync(
            IIdentity id,
            int fromEventSequenceNumber,
            CancellationToken cancellationToken)
        {
            var events =
                this.collection.Find(e => e.AggregateId == id.Value
                                          && e.AggregateSequenceNumber >= fromEventSequenceNumber);

            IReadOnlyCollection<ICommittedDomainEvent> domainEvents = events.ToImmutableArray();
            return Task.FromResult(domainEvents);
        }

        private class LiteDbEvent : ICommittedDomainEvent
        {
            public ObjectId Id { get; set; }

            public string AggregateId { get; set; }

            public int AggregateSequenceNumber { get; set; }

            public string Data { get; set; }

            public string Metadata { get; set; }
        }
    }
}