using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Queries;
using EventFlow.ReadStores;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using EventFlow.ValueObjects;
using PizzaMaster.Bestellungen.Events;
using PizzaMaster.Konten;
using PizzaMaster.Konten.Commands;
using PizzaMaster.Konten.Events;
using PizzaMaster.Konten.Queries;
using PizzaMaster.Konten.ReadModels;
using PizzaMaster.Konten.ValueObjects;

namespace PizzaMaster.Bestellungen
{
    public class BezahlenSagaId : SingleValueObject<string>, ISagaId
    {
        public BezahlenSagaId(string value) : base(value) { }
    }

    public class BestellungSagaLocator : ISagaLocator
    {
        private static ISagaId CreateId(Artikel artikel)
        {
            return new BezahlenSagaId($"bezahlung-{artikel.Id.Value.Substring("artikel-".Length)}");
        }

        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var abgebuchtEvent = domainEvent.GetAggregateEvent() as AbgebuchtEvent;
            return Task.FromResult<ISagaId>(new BezahlenSagaId(""));
        }
    }

    public class BestellungsabschlussBegonnen : AggregateEvent<BezahlenSaga, BezahlenSagaId>
    {
    }
    
    public class BezahlenSaga : AggregateSaga<BezahlenSaga, BezahlenSagaId, BestellungSagaLocator>,
                                  ISagaIsStartedBy<BestellungAggregate, BestellungId, BezahlungAngefordertEvent>,
                                  IEmit<BestellungsabschlussBegonnen>,
                                  ISagaHandles<KontoAggregate, KontoId, AbgebuchtEvent>,
                                  ISagaHandles<KontoAggregate, KontoId, KontoEroeffnetEvent>
    {
        private readonly IQueryProcessor queryProcessor;

        public BezahlenSaga(BezahlenSagaId id, IQueryProcessor queryProcessor) : base(id)
        {
            this.queryProcessor = queryProcessor;
        }

        public async Task HandleAsync(IDomainEvent<BestellungAggregate, BestellungId, BezahlungAngefordertEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var artikel = domainEvent.AggregateEvent.Artikel;

            var konto = await this.queryProcessor.ProcessAsync(new KontoByBenutzerQuery(artikel.Benutzer, false), cancellationToken);

            if (konto == null)
            {
                this.Publish(new KontoEroeffnenCommand(artikel.Benutzer));
            }
            else
            {
                this.Publish(AbbuchenCommand.FromArtikel(konto.Id, artikel));
            }
        }

        public void Apply(BestellungsabschlussBegonnen aggregateEvent)
        {
            
        }

        public Task HandleAsync(IDomainEvent<KontoAggregate, KontoId, AbgebuchtEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task HandleAsync(IDomainEvent<KontoAggregate, KontoId, KontoEroeffnetEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}