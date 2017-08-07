using EventFlow.Commands;
using EventFlow.Core;

namespace PizzaMaster.Konten.Commands
{
    public class KontoAufloesenCommand : Command<KontoAggregate, KontoId>
    {
        public KontoAufloesenCommand(KontoId aggregateId) : base(aggregateId) { }
    }
}