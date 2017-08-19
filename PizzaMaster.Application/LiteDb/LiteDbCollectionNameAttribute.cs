using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaMaster.Application.LiteDb
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LiteDbCollectionNameAttribute : Attribute
    {
        public LiteDbCollectionNameAttribute(string collectionName)
        {
            this.CollectionName = collectionName;
        }

        public virtual string CollectionName { get; }
    }
}