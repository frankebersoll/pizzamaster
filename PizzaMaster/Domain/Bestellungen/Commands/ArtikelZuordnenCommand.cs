using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;
using PizzaMaster.Domain.Bestellungen.Entities;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Bestellungen.Commands
{
    public class ArtikelZuordnenCommand : Command<BestellungAggregate, BestellungId>
    {
        public ArtikelZuordnenCommand(BestellungId aggregateId, ArtikelId artikel, Benutzer benutzer) :
            base(aggregateId)
        {
            this.Artikel = artikel ?? throw new ArgumentNullException(nameof(artikel));
            this.Benutzer = benutzer ?? throw new ArgumentNullException(nameof(benutzer));
        }

        public ArtikelId Artikel { get; }

        public Benutzer Benutzer { get; }
    }
}