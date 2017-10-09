using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using EventFlow.ValueObjects;
using LiteDB;

namespace PizzaMaster.Application.Serialization
{
    public static class BsonMapping
    {
        public static void MapSimpleValueObjects(this BsonMapper mapper)
        {
            var simpleValueObjectTypes = from type in PizzaMaster.PizzaMasterAssembly.ExportedTypes
                                         where type.IsClass && !type.IsAbstract
                                         where type.IsAssignableTo<ISingleValueObject>()
                                         select type;

            foreach (var type in simpleValueObjectTypes)
            {
                mapper.RegisterType(type,
                                    o => new BsonValue(((ISingleValueObject) o).GetValue()),
                                    v => Activator.CreateInstance(type, v.RawValue));
            }
        }
    }
}