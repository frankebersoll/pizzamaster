using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace PizzaMaster.Application.Serialization
{
    public class ReadOnlyPropertyValueProvider : ExpressionValueProvider, IValueProvider
    {
        private readonly ExpressionValueProvider setProvider;

        public ReadOnlyPropertyValueProvider(PropertyInfo p) : base(p)
        {
            var field = p.DeclaringType
                         .GetTypeInfo()
                         .GetField($"<{p.Name}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);

            this.setProvider = new ExpressionValueProvider(field);
        }

        void IValueProvider.SetValue(object target, object value)
        {
            this.setProvider.SetValue(target, value);
        }
    }
}