using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Specifications;

namespace PizzaMaster.Domain
{
    public class ForAllSpecification<T> : Specification<IEnumerable<T>>
    {
        private readonly ISpecification<T> inner;

        public ForAllSpecification(ISpecification<T> specification)
        {
            this.inner = specification;
        }

        protected override IEnumerable<string> IsNotSatisfiedBecause(IEnumerable<T> obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return obj.SelectMany(o => this.inner.WhyIsNotSatisfiedBy(o));
        }
    }
}