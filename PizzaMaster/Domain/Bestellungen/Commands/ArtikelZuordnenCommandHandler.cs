using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    class ArtikelZuordnenCommandHandler : CommandHandler<BestellungAggregate, BestellungId, ArtikelZuordnenCommand>
    {
        public override Task ExecuteAsync(
            BestellungAggregate aggregate,
            ArtikelZuordnenCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.ArtikelZuordnen(command.Artikel, command.Benutzer);
            return Task.CompletedTask;
        }
    }
}