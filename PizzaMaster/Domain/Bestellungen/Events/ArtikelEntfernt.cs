using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Domain.Bestellungen.Entities;

namespace PizzaMaster.Domain.Bestellungen.Events
{
    public class ArtikelEntfernt : AggregateEvent<BestellungAggregate, BestellungId>
    {
        public ArtikelEntfernt(ArtikelId artikelId)
        {
            this.ArtikelId = artikelId;
        }

        public ArtikelId ArtikelId { get; }
    }
}