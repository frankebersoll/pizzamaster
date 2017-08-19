using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Specifications;

namespace PizzaMaster.Domain.Bestellungen
{
    public static class BestellungSpecs
    {
        public static readonly ISpecification<BestellungAggregate> NichtAbgeschlossen
            = new NichtAbgeschlossenSpec();

        private class NichtAbgeschlossenSpec : Specification<BestellungAggregate>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(BestellungAggregate obj)
            {
                if (obj.IstAbgeschlossen)
                {
                    yield return "Bestellung ist bereits abgeschlossen.";
                }
            }
        }
    }
}