using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Domain.Bestellungen.Entities;

namespace PizzaMaster.Domain.Bestellungen.Events
{
    public class ArtikelHinzugefuegt : AggregateEvent<BestellungAggregate, BestellungId>
    {
        public ArtikelHinzugefuegt(Artikel artikel)
        {
            this.Artikel = artikel;
        }

        public Artikel Artikel { get; }
    }
}