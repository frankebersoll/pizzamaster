using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.EventStores.Files;
using EventFlow.Extensions;
using EventFlow.Queries;
using EventFlow.ReadStores;
using PizzaMaster.Bestellungen.Commands;
using PizzaMaster.Konten;
using PizzaMaster.Konten.Commands;
using PizzaMaster.Konten.Queries;
using PizzaMaster.Konten.ReadModels;
using PizzaMaster.Konten.ValueObjects;
using Xunit;

namespace PizzaMaster
{
    public class Tests : IDisposable
    {
        private readonly IRootResolver resolver;
        private readonly ICommandBus commandBus;

        public Tests()
        {
            this.resolver = EventFlowOptions.New
                                            .UseFilesEventStore(FilesEventStoreConfiguration.Create(@".\Data\"))
                                            .UseInMemoryReadStoreFor<KontoReadModel>()
                                            .AddDefaults(typeof(Tests).Assembly)
                                            .CreateResolver();

            this.commandBus = this.resolver.Resolve<ICommandBus>();
        }

        [Fact]
        public async void FactName()
        {
            var populator = this.resolver.Resolve<IReadModelPopulator>();
            await populator.PopulateAsync<KontoReadModel>(CancellationToken.None);
            

            var bestellungCommand = new BestellungBeginnenCommand("Testbestellung");
            await this.commandBus.PublishAsync(bestellungCommand, CancellationToken.None);

            var bestellung = bestellungCommand.AggregateId;

            var benni = new Benutzer("Benni");

            await this.commandBus.PublishAsync(new ArtikelHinzufuegenCommand(bestellung, benni, 20, "Diavolo"),
                                         CancellationToken.None);
            
            await this.commandBus.PublishAsync(new ArtikelHinzufuegenCommand(bestellung, benni, 10, "Tortellini"),
                                         CancellationToken.None);

            await this.commandBus.PublishAsync(new BestellungAbschliessenCommand(bestellung), CancellationToken.None);
            
            var processor = this.resolver.Resolve<IQueryProcessor>();
            var bennisKonto = await processor.ProcessAsync(new KontoByBenutzerQuery(benni),
                                                     CancellationToken.None);
        }

        private async Task<KontoId> KontoEroeffnen(Benutzer benutzer)
        {
            var createAccountCommand = new KontoEroeffnenCommand(benutzer);
            var id = createAccountCommand.AggregateId;

            await this.commandBus.PublishAsync(createAccountCommand, CancellationToken.None);

            return id;
        }

        public void Dispose()
        {
            this.resolver.Dispose();
        }
    }
}
