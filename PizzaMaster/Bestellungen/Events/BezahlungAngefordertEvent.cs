using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;

namespace PizzaMaster.Bestellungen.Events
{
    public class BezahlungAngefordertEvent : AggregateEvent<BestellungAggregate, BestellungId>
    {
        public Artikel Artikel { get; }

        public BezahlungAngefordertEvent(Artikel artikel)
        {
            this.Artikel = artikel;
        }
    }
}