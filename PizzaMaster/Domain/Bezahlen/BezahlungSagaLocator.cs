using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Sagas;
using PizzaMaster.Domain.Bestellungen;
using PizzaMaster.Domain.Bestellungen.Events;
using PizzaMaster.Domain.Konten.Events;

namespace PizzaMaster.Domain.Bezahlen
{
    public class BezahlungSagaLocator : ISagaLocator
    {
        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var e = domainEvent.GetAggregateEvent();
            if (e is BezahlungAngefordert angefordertEvent)
            {
                var bestellung = (BestellungId) domainEvent.GetIdentity();
                ISagaId id = new BezahlungId(bestellung, angefordertEvent.Benutzer);
                return Task.FromResult(id);
            }

            if (e is Abgebucht || e is KontoEroeffnet)
            {
                return Task.FromResult(this.CreateId(domainEvent));
            }

            return Task.FromResult(BezahlungId.Empty);
        }

        private ISagaId CreateId(IDomainEvent domainEvent)
        {
            var id = domainEvent.TryGetBezahlung();
            return id ?? BezahlungId.Empty;
        }
    }
}