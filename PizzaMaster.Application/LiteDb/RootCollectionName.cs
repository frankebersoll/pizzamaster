using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.ValueObjects;

namespace PizzaMaster.Application.LiteDb
{
    public class RootCollectionName : SingleValueObject<string>
    {
        public RootCollectionName(string value) : base(value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
        }
    }
}