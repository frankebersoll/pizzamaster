using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    public class ArtikelEntfernenCommandHandler
        : CommandHandler<BestellungAggregate, BestellungId, ArtikelEntfernenCommand>
    {
        public override Task ExecuteAsync(
            BestellungAggregate aggregate,
            ArtikelEntfernenCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.ArtikelEntfernen(command.ArtikelId);
            return Task.CompletedTask;
        }
    }
}