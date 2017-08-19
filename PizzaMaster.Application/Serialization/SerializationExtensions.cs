using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow;
using EventFlow.Core;

namespace PizzaMaster.Application.Serialization
{
    public static class SerializationExtensions
    {
        public static IEventFlowOptions UseSimpleJsonSerialization(this IEventFlowOptions options)
        {
            return options.RegisterServices(s => s.Register<IJsonSerializer, SimpleJsonSerializer>());
        }
    }
}