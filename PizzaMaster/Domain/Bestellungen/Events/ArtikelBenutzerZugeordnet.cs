using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using PizzaMaster.Domain.Bestellungen.Entities;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Bestellungen.Events
{
    public class ArtikelBenutzerZugeordnet : AggregateEvent<BestellungAggregate, BestellungId>
    {
        public ArtikelBenutzerZugeordnet(ArtikelId artikelId, Benutzer benutzer)
        {
            this.ArtikelId = artikelId;
            this.Benutzer = benutzer;
        }

        public ArtikelId ArtikelId { get; }

        public Benutzer Benutzer { get; }
    }
}