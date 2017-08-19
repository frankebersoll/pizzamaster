using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EventFlow;
using EventFlow.Extensions;

namespace PizzaMaster
{
    public static class PizzaMaster
    {
        public static Assembly PizzaMasterAssembly => typeof(PizzaMaster).GetTypeInfo().Assembly;

        public static IEventFlowOptions AddPizzaMasterDomain(this IEventFlowOptions options)
        {
            return options.AddDefaults(PizzaMasterAssembly);
        }
    }
}