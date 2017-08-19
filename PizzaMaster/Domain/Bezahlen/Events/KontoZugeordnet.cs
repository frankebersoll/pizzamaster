using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Domain.Konten;

namespace PizzaMaster.Domain.Bezahlen.Events
{
    public class KontoZugeordnet : AggregateEvent<BezahlungSaga, BezahlungId>
    {
        public KontoZugeordnet(KontoId kontoId)
        {
            this.KontoId = kontoId;
        }

        public KontoId KontoId { get; }
    }
}