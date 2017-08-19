using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    public class
        BezahlungAbschliessenCommandHandler : CommandHandler<BestellungAggregate, BestellungId,
            BezahlungAbschliessenCommand>
    {
        public override Task ExecuteAsync(
            BestellungAggregate aggregate,
            BezahlungAbschliessenCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.BezahlungAbschliessen(command.Bezahlung.Benutzer);
            return Task.CompletedTask;
        }
    }
}