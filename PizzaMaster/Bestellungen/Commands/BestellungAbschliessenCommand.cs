using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace PizzaMaster.Bestellungen.Commands
{
    public class BestellungAbschliessenCommand : Command<BestellungAggregate, BestellungId>
    {
        public BestellungAbschliessenCommand(BestellungId aggregateId) : base(aggregateId) { }
    }
    
    public class BestellungAbschliessenCommandHandler : CommandHandler<BestellungAggregate, BestellungId, BestellungAbschliessenCommand>
    {
        public override Task ExecuteAsync(
            BestellungAggregate aggregate,
            BestellungAbschliessenCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.Abschliessen();
            return Task.FromResult(true);
        }
    }
}