using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace PizzaMaster.Bestellungen.Commands
{
    public class BestellungBeginnenCommandHandler : CommandHandler<BestellungAggregate, BestellungId, BestellungBeginnenCommand>
    {
        public override Task ExecuteAsync(
            BestellungAggregate aggregate,
            BestellungBeginnenCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.Beginnen(command.Lieferdienst);
            return Task.FromResult(true);
        }
    }
}