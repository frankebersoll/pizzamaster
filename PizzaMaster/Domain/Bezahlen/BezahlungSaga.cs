using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Queries;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using PizzaMaster.Domain.Bestellungen;
using PizzaMaster.Domain.Bestellungen.Commands;
using PizzaMaster.Domain.Bestellungen.Events;
using PizzaMaster.Domain.Bezahlen.Events;
using PizzaMaster.Domain.Common;
using PizzaMaster.Domain.Konten;
using PizzaMaster.Domain.Konten.Commands;
using PizzaMaster.Domain.Konten.Events;
using PizzaMaster.Query.Konten;

namespace PizzaMaster.Domain.Bezahlen
{
    public class BezahlungSaga : AggregateSaga<BezahlungSaga, BezahlungId, BezahlungSagaLocator>,
                                 ISagaIsStartedBy<BestellungAggregate, BestellungId, BezahlungAngefordert>,
                                 ISagaHandles<KontoAggregate, KontoId, Abgebucht>,
                                 ISagaHandles<KontoAggregate, KontoId, KontoEroeffnet>
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly BezahlungState state = new BezahlungState();

        public BezahlungSaga(BezahlungId id, IQueryProcessor queryProcessor) : base(id)
        {
            this.queryProcessor = queryProcessor;

            this.Register(this.state);
        }

        public async Task HandleAsync(
            IDomainEvent<BestellungAggregate, BestellungId, BezahlungAngefordert> domainEvent,
            ISagaContext sagaContext,
            CancellationToken cancellationToken)
        {
            var angefordert = domainEvent.AggregateEvent;
            var betrag = angefordert.Betrag;
            var beschreibung = angefordert.Beschreibung;
            var benutzer = angefordert.Benutzer;
            var konto = await this.queryProcessor.ProcessAsync(new KontoByBenutzerQuery(benutzer, false),
                                                               cancellationToken);

            this.Emit(new BezahlungBegonnen(beschreibung, betrag));

            if (konto == null)
            {
                this.Publish(new KontoEroeffnenCommand(this.Id));
            }
            else
            {
                this.Emit(new KontoZugeordnet(new KontoId(konto.Id)));
                this.PublishAbbuchenCommand(new KontoId(konto.Id), beschreibung, betrag);
            }
        }

        public Task HandleAsync(
            IDomainEvent<KontoAggregate, KontoId, Abgebucht> domainEvent,
            ISagaContext sagaContext,
            CancellationToken cancellationToken)
        {
            this.Emit(new BezahlungAbgeschlossen());
            this.Publish(new BezahlungAbschliessenCommand(this.Id));
            this.Complete();
            return Task.CompletedTask;
        }

        public Task HandleAsync(
            IDomainEvent<KontoAggregate, KontoId, KontoEroeffnet> domainEvent,
            ISagaContext sagaContext,
            CancellationToken cancellationToken)
        {
            var kontoId = domainEvent.AggregateIdentity;
            this.Emit(new KontoZugeordnet(kontoId));
            this.PublishAbbuchenCommand(kontoId, this.state.Beschreibung, this.state.Betrag);
            return Task.CompletedTask;
        }

        private void PublishAbbuchenCommand(KontoId konto, string beschreibung, Betrag betrag)
        {
            var command = new AbbuchenCommand(konto, betrag, beschreibung, this.Id);
            this.Publish(command);
        }
    }
}