using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EventFlow.Extensions;
using PizzaMaster.Query;

namespace PizzaMaster.Application.LiteDb
{
    public class ReadModelDescriptionProvider : IReadModelDescriptionProvider
    {
        private static readonly ConcurrentDictionary<Type, ReadModelDescription> CollectionNames
            = new ConcurrentDictionary<Type, ReadModelDescription>();

        public ReadModelDescription GetReadModelDescription<TReadModel>() where TReadModel : IVersionedReadModel
        {
            return CollectionNames
                .GetOrAdd(typeof(TReadModel),
                          t =>
                          {
                              var collectionType = t.GetTypeInfo()
                                                    .GetCustomAttribute<LiteDbCollectionNameAttribute>();
                              var indexName = collectionType == null
                                                  ? $"eventflow-{typeof(TReadModel).PrettyPrint().ToLowerInvariant()}"
                                                  : collectionType.CollectionName;
                              return new ReadModelDescription(new RootCollectionName(indexName));
                          });
        }
    }
}