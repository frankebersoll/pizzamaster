using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PizzaMaster.Application.Serialization
{
    public class ReadOnlyPropertyResolver : DefaultContractResolver
    {
        private readonly Type valueObjectType;

        public ReadOnlyPropertyResolver(Type valueObjectType)
        {
            this.valueObjectType = valueObjectType;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            if (!this.valueObjectType.GetTypeInfo().IsAssignableFrom(type))
            {
                return base.CreateProperties(type, memberSerialization);
            }

            List<JsonProperty> properties
                = type.GetTypeInfo().GetProperties()
                      .Select(p =>
                      {
                          JsonProperty jsonProperty =
                              this.CreateProperty(p, memberSerialization);
                          jsonProperty.Readable = true;
                          jsonProperty.Writable = true;
                          if (p.SetMethod == null)
                          {
                              jsonProperty.ValueProvider =
                                  new ReadOnlyPropertyValueProvider(p);
                          }

                          return jsonProperty;
                      })
                      .ToList();

            return properties;
        }
    }
}