using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Specifications;
using PizzaMaster.Domain.Bestellungen.Entities;

namespace PizzaMaster.Domain.Bestellungen
{
    public static class ArtikelSpecs
    {
        public static readonly ISpecification<Artikel> Zugeordnet = new ZugeordnetSpec();

        public static ISpecification<Artikel> Status(ArtikelStatus status) => new StatusSpec(status);

        public class StatusSpec : Specification<Artikel>
        {
            private readonly ArtikelStatus status;

            public StatusSpec(ArtikelStatus status)
            {
                this.status = status;
            }

            protected override IEnumerable<string> IsNotSatisfiedBecause(Artikel obj)
            {
                if (obj.Status != this.status)
                {
                    yield return $"Artikel ist nicht im Zustand {this.status}.";
                }
            }
        }

        public class ZugeordnetSpec : Specification<Artikel>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(Artikel artikel)
            {
                if (!artikel.IstZugeordnet)
                {
                    yield return "Artikel wurden keinem Benutzer zugeordnet.";
                }
            }
        }
    }
}