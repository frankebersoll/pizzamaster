using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using Autofac;
using EventFlow;
using EventFlow.Commands;
using EventFlow.Extensions;
using EventFlow.Queries;
using EventFlow.ReadStores;
using PizzaMaster.Domain.Bestellungen.Commands;
using PizzaMaster.Domain.Common;
using PizzaMaster.Domain.Konten;
using PizzaMaster.Domain.Konten.Commands;
using PizzaMaster.Query.Bestellungen;
using PizzaMaster.Query.Konten;

namespace PizzaMaster.Application.Client
{
    public class PizzaMasterClient : IDisposable
    {
        private readonly ICommandBus commandBus;
        private readonly ILifetimeScope container;
        private readonly IReadModelPopulator populator;
        private readonly IQueryProcessor queryProcessor;

        public PizzaMasterClient(
            ICommandBus commandBus,
            IQueryProcessor queryProcessor,
            IReadModelPopulator populator,
            ILifetimeScope container)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
            this.populator = populator;
            this.container = container;
        }

        public void Dispose()
        {
            this.container.Dispose();
        }

        public BestellungModel BestellungBeginnen(string lieferdienst, DateTime datum = default(DateTime))
        {
            var command = new BestellungBeginnenCommand(lieferdienst, datum);
            this.Publish(command);
            return new BestellungModel(command.AggregateId, this);
        }

        public Benutzer[] GetBenutzer()
        {
            return this.Query(new GetBenutzerQuery());
        }

        public IEnumerable<BestellungModel> GetBestellungen()
        {
            var entities = this.Query(new GetAllBestellungenQuery());
            return entities.Select(e => new BestellungModel(e, this));
        }

        public IEnumerable<KontoModel> GetKonten()
        {
            var entities = this.Query(new GetAllKontenQuery());
            return entities.Select(e => new KontoModel(e, this));
        }

        public KontoModel GetOrCreateKonto(Benutzer benutzer)
        {
            return this.TryGetKonto(benutzer) ?? this.KontoEroeffnen(benutzer);
        }

        public KontoModel KontoEroeffnen(Benutzer benutzer)
        {
            var command = new KontoEroeffnenCommand(benutzer);
            this.Publish(command);
            return new KontoModel(command.AggregateId, this);
        }

        public void Publish(params ICommand[] commands)
        {
            try
            {
                foreach (var command in commands)
                {
                    command.PublishAsync(this.commandBus, CancellationToken.None).Wait();
                }
            }
            catch (AggregateException e)
            {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                throw;
            }
        }

        [DebuggerStepThrough]
        public TResult Query<TResult>(IQuery<TResult> query)
        {
            try
            {
                return this.queryProcessor.ProcessAsync(query, CancellationToken.None).Result;
            }
            catch (AggregateException e)
            {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                throw;
            }
        }

        public void ResetReadModels()
        {
            this.populator.Purge<BenutzerReadModel>(CancellationToken.None);
            this.populator.Purge<BestellungReadModel>(CancellationToken.None);
            this.populator.Purge<KontoReadModel>(CancellationToken.None);
            this.populator.Populate<BenutzerReadModel>(CancellationToken.None);
            this.populator.Populate<BestellungReadModel>(CancellationToken.None);
            this.populator.Populate<KontoReadModel>(CancellationToken.None);
        }

        public KontoModel TryGetKonto(Benutzer benutzer)
        {
            var model = this.Query(new KontoByBenutzerQuery(benutzer, false));
            return model != null ? new KontoModel(new KontoId(model.Id), this) : null;
        }
    }
}