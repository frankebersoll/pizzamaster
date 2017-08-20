using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    public class BestellungAbbrechenCommandHandler
        : CommandHandler<BestellungAggregate, BestellungId, BestellungAbbrechenCommand>
    {
        public override Task ExecuteAsync(
            BestellungAggregate aggregate,
            BestellungAbbrechenCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.Abbrechen();
            return Task.CompletedTask;
        }
    }
}