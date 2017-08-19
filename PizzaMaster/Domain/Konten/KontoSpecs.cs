using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Specifications;

namespace PizzaMaster.Domain.Konten
{
    public static class KontoSpecs
    {
        public static readonly ISpecification<KontoAggregate> Ausgeglichen = new AusgeglichenSpec();
        public static readonly ISpecification<KontoAggregate> NichtAufgeloest = new NichtAufgeloestSpec();

        private class AusgeglichenSpec : Specification<KontoAggregate>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(KontoAggregate obj)
            {
                if (obj.Saldo != 0m)
                {
                    yield return string.Format("Saldo {0:F2} ist nicht ausgeglichen.", obj.Saldo);
                }
            }
        }

        private class NichtAufgeloestSpec : Specification<KontoAggregate>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(KontoAggregate obj)
            {
                if (obj.IstAufgeloest)
                {
                    yield return "Konto ist aufgelöst.";
                }
            }
        }
    }
}