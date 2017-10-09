using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using PizzaMaster.Domain.Common;
using PizzaMaster.Domain.Konten;
using PizzaMaster.Domain.Konten.Events;

namespace PizzaMaster.Query.Konten
{
    public class BenutzerReadModel : IVersionedReadModel,
                                     IAmReadModelFor<KontoAggregate, KontoId, KontoEroeffnet>,
                                     IAmReadModelFor<KontoAggregate, KontoId, KontoAufgeloest>
    {
        public Benutzer[] Benutzer => this.BenutzerByKontoId.Values.ToArray();

        public Dictionary<string, Benutzer> BenutzerByKontoId { get; private set; } =
            new Dictionary<string, Benutzer>();

        public void Apply(IReadModelContext context, IDomainEvent<KontoAggregate, KontoId, KontoAufgeloest> domainEvent)
        {
            this.BenutzerByKontoId.Remove(domainEvent.AggregateIdentity.Value);
        }

        public void Apply(IReadModelContext context, IDomainEvent<KontoAggregate, KontoId, KontoEroeffnet> domainEvent)
        {
            this.BenutzerByKontoId[domainEvent.AggregateIdentity.Value] = domainEvent.AggregateEvent.Benutzer;
        }

        public string Id => "Benutzer";

        public long? Version { get; set; }
    }
}