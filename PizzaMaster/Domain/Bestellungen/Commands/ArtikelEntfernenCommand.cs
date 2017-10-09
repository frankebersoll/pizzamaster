using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;
using PizzaMaster.Domain.Bestellungen.Entities;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    public class ArtikelEntfernenCommand : Command<BestellungAggregate, BestellungId>
    {
        public ArtikelEntfernenCommand(BestellungId aggregateId, ArtikelId artikelId) : base(aggregateId)
        {
            this.ArtikelId = artikelId ?? throw new ArgumentNullException(nameof(artikelId));
        }

        public ArtikelId ArtikelId { get; }
    }
}