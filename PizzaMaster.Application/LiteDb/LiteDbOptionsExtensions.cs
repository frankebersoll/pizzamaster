using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.EventStores;
using EventFlow.Extensions;
using EventFlow.ReadStores;
using LiteDB;
using PizzaMaster.Query;

namespace PizzaMaster.Application.LiteDb
{
    public static class LiteDbOptionsExtensions
    {
        public static IEventFlowOptions ConfigureLiteDb(
            this IEventFlowOptions eventFlowOptions,
            string connectionString,
            BsonMapper mapper = null)
        {
            return eventFlowOptions.ConfigureLiteDb(() => new LiteDatabase(connectionString, mapper));
        }

        public static IEventFlowOptions ConfigureLiteDb(
            this IEventFlowOptions eventFlowOptions,
            Func<LiteDatabase> mongoDatabaseFactory)
        {
            return eventFlowOptions.RegisterServices(sr =>
            {
                sr.Register(f => mongoDatabaseFactory(), Lifetime.Singleton);
                sr.Register<IReadModelDescriptionProvider, ReadModelDescriptionProvider>(Lifetime.Singleton, true);
            });
        }

        public static IEventFlowOptions UseLiteDbEventPersistance(
            this IEventFlowOptions eventFlowOptions,
            string collectionName = "eventflow-events")
        {
            return eventFlowOptions.RegisterServices(s => s.Register<IEventPersistence>(
                                                                                        c =>
                                                                                            new
                                                                                                LiteDbEventPersistance(c.Resolver.Resolve<LiteDatabase>(),
                                                                                                                       collectionName)));
        }

        public static IEventFlowOptions UseLiteDbReadStoreFor<TReadModel>(
            this IEventFlowOptions eventFlowOptions)
            where TReadModel : class, IVersionedReadModel, new()
        {
            return eventFlowOptions
                .RegisterServices(f =>
                {
                    f.Register<ISearchableReadModelStore<TReadModel>, LiteDbReadModelStore<TReadModel>>();
                    f.Register<IReadModelStore<TReadModel>>(r =>
                                                                r.Resolver
                                                                 .Resolve<ISearchableReadModelStore<TReadModel>>());
                })
                .UseReadStoreFor<ISearchableReadModelStore<TReadModel>, TReadModel>();
        }

        public static IEventFlowOptions UseLiteDbReadStoreFor<TReadModel, TReadModelLocator>(
            this IEventFlowOptions eventFlowOptions)
            where TReadModel : class, IVersionedReadModel, new()
            where TReadModelLocator : IReadModelLocator
        {
            return eventFlowOptions
                .RegisterServices(f =>
                {
                    f.Register<ISearchableReadModelStore<TReadModel>, LiteDbReadModelStore<TReadModel>>();
                    f.Register<IReadModelStore<TReadModel>>(r =>
                                                                r.Resolver
                                                                 .Resolve<ISearchableReadModelStore<TReadModel>>());
                })
                .UseReadStoreFor<ISearchableReadModelStore<TReadModel>, TReadModel, TReadModelLocator>();
        }
    }
}