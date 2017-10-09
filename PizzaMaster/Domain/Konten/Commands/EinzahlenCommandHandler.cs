using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace PizzaMaster.Domain.Konten.Commands
{
    class EinzahlenCommandHandler : CommandHandler<KontoAggregate, KontoId, EinzahlenCommand>
    {
        public override Task ExecuteAsync(
            KontoAggregate aggregate,
            EinzahlenCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.Einzahlen(command.Betrag, command.Einzahlungsart);
            return Task.CompletedTask;
        }
    }
}