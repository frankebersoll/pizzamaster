using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;
using EventFlow.ReadStores;
using EventFlow.ReadStores.InMemory;
using PizzaMaster.Query;

namespace PizzaMaster.Tests.Extensions
{
    public static class ConfigExtensions
    {
        public static IEventFlowOptions UseTestReadStoreFor<TReadModel>(
            this IEventFlowOptions eventFlowOptions)
            where TReadModel : class, IVersionedReadModel, new()
        {
            return eventFlowOptions
                .RegisterServices(f =>
                {
                    f.Register<InMemoryReadStore<TReadModel>, InMemoryReadStore<TReadModel>>(Lifetime.Singleton);
                    f.Register<ISearchableReadModelStore<TReadModel>, TestReadStore<TReadModel>>();
                    f.Register<IReadModelStore<TReadModel>>(r =>
                                                                r.Resolver
                                                                 .Resolve<ISearchableReadModelStore<TReadModel>>());
                })
                .UseReadStoreFor<ISearchableReadModelStore<TReadModel>, TReadModel>();
        }

        public static IEventFlowOptions UseTestReadStoreFor<TReadModel, TReadModelLocator>(
            this IEventFlowOptions eventFlowOptions)
            where TReadModel : class, IVersionedReadModel, new()
            where TReadModelLocator : IReadModelLocator
        {
            return eventFlowOptions
                .RegisterServices(f =>
                {
                    f.Register<InMemoryReadStore<TReadModel>, InMemoryReadStore<TReadModel>>(Lifetime.Singleton);
                    f.Register<ISearchableReadModelStore<TReadModel>, TestReadStore<TReadModel>>();
                    f.Register<IReadModelStore<TReadModel>>(r =>
                                                                r.Resolver
                                                                 .Resolve<ISearchableReadModelStore<TReadModel>>());
                })
                .UseReadStoreFor<ISearchableReadModelStore<TReadModel>, TReadModel, TReadModelLocator>();
        }
    }
}