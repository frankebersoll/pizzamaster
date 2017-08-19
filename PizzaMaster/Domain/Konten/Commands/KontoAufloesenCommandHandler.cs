using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace PizzaMaster.Domain.Konten.Commands
{
    class KontoAufloesenCommandHandler : CommandHandler<KontoAggregate, KontoId, KontoAufloesenCommand>
    {
        public override Task ExecuteAsync(
            KontoAggregate aggregate,
            KontoAufloesenCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.Aufloesen();
            return Task.CompletedTask;
        }
    }
}