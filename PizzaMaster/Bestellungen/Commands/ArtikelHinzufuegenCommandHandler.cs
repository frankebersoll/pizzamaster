using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Commands;
using EventFlow.Queries;
using EventFlow.Subscribers;
using PizzaMaster.Konten.Queries;

namespace PizzaMaster.Bestellungen.Commands {
    public class ArtikelHinzufuegenCommandHandler : CommandHandler<BestellungAggregate, BestellungId, ArtikelHinzufuegenCommand>
    {
        public override Task ExecuteAsync(BestellungAggregate aggregate, ArtikelHinzufuegenCommand command, CancellationToken cancellationToken)
        {
            aggregate.ArtikelHinzufuegen(command.Benutzer, command.Betrag, command.Beschreibung);
            return Task.FromResult(true);
        }
    }
}