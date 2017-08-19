using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;

namespace PizzaMaster.Domain.Konten.Commands
{
    public class KontoAufloesenCommand : Command<KontoAggregate, KontoId>
    {
        public KontoAufloesenCommand(KontoId aggregateId) : base(aggregateId) { }
    }
}