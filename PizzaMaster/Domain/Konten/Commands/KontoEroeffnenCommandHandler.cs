using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventFlow.Exceptions;
using EventFlow.Queries;
using PizzaMaster.Domain.Bezahlen;
using PizzaMaster.Query.Konten;

namespace PizzaMaster.Domain.Konten.Commands
{
    class KontoEroeffnenCommandHandler : CommandHandler<KontoAggregate, KontoId, KontoEroeffnenCommand>
    {
        private readonly IQueryProcessor queryProcessor;

        public KontoEroeffnenCommandHandler(IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }

        public override async Task ExecuteAsync(
            KontoAggregate aggregate,
            KontoEroeffnenCommand command,
            CancellationToken cancellationToken)
        {
            var query = new KontoByBenutzerQuery(command.Benutzer, false);
            if (await this.queryProcessor.ProcessAsync(query, cancellationToken) != null)
            {
                throw DomainError.With($"Konto für Benutzer {command.Benutzer} existiert bereits.");
            }

            aggregate.Eroeffnen(command.Benutzer);

            if (command.Bezahlung != null)
            {
                aggregate.BezahlungZuordnen(command.Bezahlung);
            }
        }
    }
}