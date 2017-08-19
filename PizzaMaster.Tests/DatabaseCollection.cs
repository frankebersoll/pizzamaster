using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PizzaMaster.Tests
{
    [CollectionDefinition("db")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture> { }
}