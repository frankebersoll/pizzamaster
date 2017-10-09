using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using EventFlow;
using EventFlow.Autofac.Extensions;
using EventFlow.Configuration;
using EventFlow.Core.Caching;
using EventFlow.Logs;
using LiteDB;
using PizzaMaster.Application.Client;
using PizzaMaster.Application.LiteDb;
using PizzaMaster.Application.Serialization;
using PizzaMaster.Query.Bestellungen;
using PizzaMaster.Query.Konten;

namespace PizzaMaster.Application
{
    public class PizzaMasterApplication : IConfiguredApplication
    {
        private readonly Lazy<IContainer> container;
        private readonly IEventFlowOptions options;

        private PizzaMasterApplication()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<PizzaMasterClient>().AsSelf();

            this.options = EventFlowOptions.New
                                           .UseAutofacContainerBuilder(builder)
                                           .AddPizzaMasterDomain()
                                           .RegisterServices(s =>
                                                                 s.Register<IMemoryCache, DictionaryMemoryCache
                                                                 >(Lifetime.Singleton))
                                           .UseSimpleJsonSerialization();

            this.container = new Lazy<IContainer>(() => this.options.CreateContainer());
        }

        public IRun UseLog(ILog log)
        {
            this.options.RegisterServices(s => s.Register(c => log, Lifetime.Singleton));
            return this;
        }

        PizzaMasterClient IRun.Run()
        {
            return this.container.Value.Resolve<PizzaMasterClient>();
        }

        public IConfiguredApplication Configure(Action<IEventFlowOptions> configuration)
        {
            configuration(this.options);
            return this;
        }

        public IConfiguredApplication ConfigureLiteDb(string connectionString = "PizzaMaster.db")
        {
            this.options.ConfigureLiteDb(connectionString, CreateBsonMapper());
            this.UseLiteDbStores();

            return this;
        }

        public IConfiguredApplication ConfigureLiteDb(Stream stream)
        {
            this.options.ConfigureLiteDb(stream, CreateBsonMapper());
            this.UseLiteDbStores();

            return this;
        }

        public static PizzaMasterApplication Create()
        {
            return new PizzaMasterApplication();
        }

        private static BsonMapper CreateBsonMapper()
        {
            var mapper = new BsonMapper();
            mapper.MapSimpleValueObjects();
            return mapper;
        }

        private void UseLiteDbStores()
        {
            this.options
                .UseLiteDbEventPersistance()
                .UseLiteDbReadStoreFor<KontoReadModel>()
                .UseLiteDbReadStoreFor<BestellungReadModel>()
                .UseLiteDbReadStoreFor<BenutzerReadModel, BenutzerReadModelLocator>()
                .RegisterServices(r => r.Register(c => new BenutzerReadModelLocator()));
        }
    }
}