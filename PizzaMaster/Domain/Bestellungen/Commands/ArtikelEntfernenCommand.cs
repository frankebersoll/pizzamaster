using System;
using EventFlow.Commands;
using PizzaMaster.Domain.Bestellungen.Entities;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    public class ArtikelEntfernenCommand : Command<BestellungAggregate, BestellungId>
    {
        public ArtikelId ArtikelId { get; }

        public ArtikelEntfernenCommand(BestellungId aggregateId, ArtikelId artikelId) : base(aggregateId)
        {
            this.ArtikelId = artikelId ?? throw new ArgumentNullException(nameof(artikelId));
        }
    }
}