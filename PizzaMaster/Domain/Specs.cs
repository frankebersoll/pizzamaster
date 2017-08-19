using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.Provided.Specifications;
using EventFlow.Specifications;

namespace PizzaMaster.Domain
{
    public class Specs
    {
        public static ISpecification<IAggregateRoot> Existiert { get; } = new AggregateIsCreatedSpecification();

        public static ISpecification<IAggregateRoot> IstNeu { get; } = new AggregateIsNewSpecification();

        private class AggregateIsCreatedSpecification : Specification<IAggregateRoot>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(IAggregateRoot obj)
            {
                if (obj.IsNew)
                {
                    yield return $"Aggregate '{obj.Name}' with ID '{obj.GetIdentity()}' is new";
                }
            }
        }
    }
}