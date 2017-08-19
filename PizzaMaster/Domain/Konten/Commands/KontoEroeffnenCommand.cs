using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;
using PizzaMaster.Domain.Bezahlen;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Domain.Konten.Commands
{
    public class KontoEroeffnenCommand : Command<KontoAggregate, KontoId>
    {
        public KontoEroeffnenCommand(Benutzer benutzer) : base(KontoId.New)
        {
            this.Benutzer = benutzer;
        }

        public KontoEroeffnenCommand(BezahlungId bezahlung) : this(bezahlung.Benutzer)
        {
            this.Bezahlung = bezahlung;
        }

        public Benutzer Benutzer { get; }

        public BezahlungId Bezahlung { get; }
    }
}