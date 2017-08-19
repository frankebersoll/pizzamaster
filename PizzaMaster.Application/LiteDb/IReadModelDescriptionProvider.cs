using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaster.Query;

namespace PizzaMaster.Application.LiteDb
{
    public interface IReadModelDescriptionProvider
    {
        ReadModelDescription GetReadModelDescription<TReadModel>()
            where TReadModel : IVersionedReadModel;
    }
}