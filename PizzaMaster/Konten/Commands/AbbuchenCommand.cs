using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventFlow.Core;
using EventFlow.Queries;
using PizzaMaster.Bestellungen;

namespace PizzaMaster.Konten.Commands
{
    public class AbbuchenCommand : Command<KontoAggregate, KontoId>
    {
        public decimal Betrag { get; }

        public string Beschreibung { get; }

        public AbbuchenCommand(KontoId aggregateId, decimal betrag, string beschreibung) : base(aggregateId)
        {
            this.Betrag = betrag;
            this.Beschreibung = beschreibung;
        }

        public static AbbuchenCommand FromArtikel(KontoId kontoId, Artikel artikel)
        {
            return new AbbuchenCommand(kontoId, artikel.Betrag, artikel.Beschreibung);
        }
    }
    
    public class AbbuchenCommandHandler : CommandHandler<KontoAggregate,KontoId,AbbuchenCommand>
    {
        public override Task ExecuteAsync(KontoAggregate aggregate, AbbuchenCommand command, CancellationToken cancellationToken)
        {
            aggregate.Abbuchen(command.Betrag, command.Beschreibung);
            return Task.FromResult(true);
        }
    }
}