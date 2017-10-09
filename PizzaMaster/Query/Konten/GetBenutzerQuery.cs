using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Queries;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.Query.Konten
{
    public class GetBenutzerQuery : IQuery<Benutzer[]> { }
}