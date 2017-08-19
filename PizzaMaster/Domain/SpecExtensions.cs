using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Specifications;

namespace PizzaMaster.Domain
{
    public static class SpecExtensions
    {
        public static ISpecification<IEnumerable<T>> ForAll<T>(this ISpecification<T> specification)
        {
            return new ForAllSpecification<T>(specification);
        }
    }
}