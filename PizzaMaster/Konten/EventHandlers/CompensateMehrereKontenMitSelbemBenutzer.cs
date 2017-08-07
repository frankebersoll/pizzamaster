using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Queries;
using EventFlow.Subscribers;
using PizzaMaster.Konten.ReadModels;
using PizzaMaster.Konten.Commands;
using PizzaMaster.Konten.Events;
using PizzaMaster.Konten.Queries;

namespace PizzaMaster.Konten.EventHandlers
{
    public class CompensateMehrereKontenMitSelbemBenutzer : ISubscribeSynchronousTo<KontoAggregate, KontoId, KontoEroeffnetEvent>
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandBus bus;

        public CompensateMehrereKontenMitSelbemBenutzer(IQueryProcessor queryProcessor, ICommandBus bus)
        {
            this.queryProcessor = queryProcessor;
            this.bus = bus;
        }

        public async Task HandleAsync(IDomainEvent<KontoAggregate, KontoId, KontoEroeffnetEvent> domainEvent, CancellationToken cancellationToken)
        {
            var benutzer = domainEvent.AggregateEvent.Benutzer;
            var query = new KontoByBenutzerQuery(benutzer);
            try
            {
                await this.queryProcessor.ProcessAsync(query, CancellationToken.None);
            }
            catch (MehrereKontenMitSelbemBenutzerException)
            {
                var id = domainEvent.AggregateIdentity;
                await this.bus.PublishAsync(new KontoAufloesenCommand(id), CancellationToken.None);
            }
        }
    }
}