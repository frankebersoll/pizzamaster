using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace PizzaMaster.Konten.Commands
{
    public class KontoAufloesenCommandHandler : CommandHandler<KontoAggregate, KontoId, KontoAufloesenCommand>
    {
        public override Task ExecuteAsync(KontoAggregate aggregate, KontoAufloesenCommand command, CancellationToken cancellationToken)
        {
            aggregate.Aufloesen();
            return Task.FromResult(true);
        }
    }
}