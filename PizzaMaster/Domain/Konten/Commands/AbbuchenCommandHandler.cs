using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using PizzaMaster.Domain.Bezahlen;

namespace PizzaMaster.Domain.Konten.Commands
{
    class AbbuchenCommandHandler : CommandHandler<KontoAggregate, KontoId, AbbuchenCommand>
    {
        public override Task ExecuteAsync(
            KontoAggregate aggregate,
            AbbuchenCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.Abbuchen(command.Betrag, command.Beschreibung);

            if (command.Bezahlung != null)
            {
                aggregate.BezahlungZuordnen(command.Bezahlung);
            }

            return Task.CompletedTask;
        }
    }
}