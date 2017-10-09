using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Queries;

namespace PizzaMaster.Query.Bestellungen
{
    public class GetAllBestellungenQuery : IQuery<IEnumerable<BestellungReadModel>> { }
}