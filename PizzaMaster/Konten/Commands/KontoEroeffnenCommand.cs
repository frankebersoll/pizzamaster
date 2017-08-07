using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;
using PizzaMaster.Konten.ValueObjects;

namespace PizzaMaster.Konten.Commands {
    public class KontoEroeffnenCommand : Command<KontoAggregate, KontoId>
    {
        public Benutzer Benutzer { get; }

        public KontoEroeffnenCommand(Benutzer benutzer) : base(KontoId.New)
        {
            this.Benutzer = benutzer;
        }
    }
}