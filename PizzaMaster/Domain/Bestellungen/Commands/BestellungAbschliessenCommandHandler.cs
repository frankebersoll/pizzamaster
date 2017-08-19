using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    public class
        BestellungAbschliessenCommandHandler : CommandHandler<BestellungAggregate, BestellungId,
            BestellungAbschliessenCommand>
    {
        public override Task ExecuteAsync(
            BestellungAggregate aggregate,
            BestellungAbschliessenCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.Abschliessen();
            return Task.CompletedTask;
        }
    }
}