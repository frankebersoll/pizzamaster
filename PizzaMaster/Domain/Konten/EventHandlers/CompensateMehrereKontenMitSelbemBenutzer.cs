using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Queries;
using EventFlow.Subscribers;
using PizzaMaster.Domain.Konten.Commands;
using PizzaMaster.Domain.Konten.Events;
using PizzaMaster.Query.Konten;

namespace PizzaMaster.Domain.Konten.EventHandlers
{
    class CompensateMehrereKontenMitSelbemBenutzer
        : ISubscribeSynchronousTo<KontoAggregate, KontoId, KontoEroeffnet>
    {
        private readonly ICommandBus bus;
        private readonly IQueryProcessor queryProcessor;

        public CompensateMehrereKontenMitSelbemBenutzer(IQueryProcessor queryProcessor, ICommandBus bus)
        {
            this.queryProcessor = queryProcessor;
            this.bus = bus;
        }

        public async Task HandleAsync(
            IDomainEvent<KontoAggregate, KontoId, KontoEroeffnet> domainEvent,
            CancellationToken cancellationToken)
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