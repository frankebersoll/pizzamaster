using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    public class ArtikelHinzufuegenCommandHandler
        : CommandHandler<BestellungAggregate, BestellungId, ArtikelHinzufuegenCommand>
    {
        public override Task ExecuteAsync(
            BestellungAggregate aggregate,
            ArtikelHinzufuegenCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.ArtikelHinzufuegen(command.Betrag, command.Beschreibung, command.Benutzer);
            return Task.CompletedTask;
        }
    }
}