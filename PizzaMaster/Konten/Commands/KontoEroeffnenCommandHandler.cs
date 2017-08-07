using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace PizzaMaster.Konten.Commands {
    public class KontoEroeffnenCommandHandler : CommandHandler<KontoAggregate, KontoId, KontoEroeffnenCommand>
    {
        public override Task ExecuteAsync(KontoAggregate aggregate, KontoEroeffnenCommand command, CancellationToken cancellationToken)
        {
            aggregate.Eroeffnen(command.Benutzer);
            return Task.FromResult(true);
        }
    }
}