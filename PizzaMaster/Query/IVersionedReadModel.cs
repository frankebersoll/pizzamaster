using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.ReadStores;

namespace PizzaMaster.Query
{
    public interface IVersionedReadModel : IReadModel
    {
        string Id { get; }

        long? Version { get; set; }
    }
}