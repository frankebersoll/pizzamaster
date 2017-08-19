using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.Core;

namespace PizzaMaster.Domain.Bezahlen
{
    public static class BezahlungExtensions
    {
        private const string BezahlungIdMetadataKey = "bezahlung-id";

        public static void BezahlungZuordnen(this IAggregateRoot aggregate, BezahlungId bezahlung)
        {
            if (bezahlung == null) throw new ArgumentNullException(nameof(bezahlung));

            foreach (var uncommittedEvent in aggregate.UncommittedEvents)
            {
                var metadata = (MetadataContainer) uncommittedEvent.Metadata;
                metadata.Add(BezahlungIdMetadataKey, bezahlung.Value);
            }
        }

        public static BezahlungId TryGetBezahlung(this IDomainEvent e)
        {
            return e.Metadata.TryGetValue(BezahlungIdMetadataKey, out string value)
                       ? new BezahlungId(value)
                       : null;
        }
    }
}