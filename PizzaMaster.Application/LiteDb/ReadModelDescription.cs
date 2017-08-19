using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.ValueObjects;

namespace PizzaMaster.Application.LiteDb
{
    public class ReadModelDescription : ValueObject
    {
        public ReadModelDescription(RootCollectionName rootCollectionName)
        {
            if (rootCollectionName == null) throw new ArgumentNullException(nameof(rootCollectionName));

            this.RootCollectionName = rootCollectionName;
        }

        public RootCollectionName RootCollectionName { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.RootCollectionName;
        }
    }
}