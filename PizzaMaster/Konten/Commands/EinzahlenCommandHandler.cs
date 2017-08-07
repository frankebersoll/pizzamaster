using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace PizzaMaster.Konten.Commands {
    public class EinzahlenCommandHandler : CommandHandler<KontoAggregate, KontoId, EinzahlenCommand>
    {
        public override Task ExecuteAsync(KontoAggregate aggregate, EinzahlenCommand command, CancellationToken cancellationToken)
        {
            aggregate.Einzahlen(command.Amount);
            return Task.FromResult(true);
        }
    }
}