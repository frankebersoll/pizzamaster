using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Queries;
using PizzaMaster.Domain.Konten;

namespace PizzaMaster.Query.Konten
{
    public class GetAllKontenQuery : IQuery<IEnumerable<KontoReadModel>> { }
}