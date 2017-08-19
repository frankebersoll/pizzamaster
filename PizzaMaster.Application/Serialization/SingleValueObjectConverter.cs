using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace PizzaMaster.Application.Serialization
{
    public class SingleValueObjectConverter : JsonConverter
    {
        private static readonly ConcurrentDictionary<Type, Type> ConstructorArgumenTypes =
            new ConcurrentDictionary<Type, Type>();

        public override bool CanConvert(Type objectType)
        {
            return typeof(ISingleValueObject).GetTypeInfo().IsAssignableFrom(objectType);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var parameterType = ConstructorArgumenTypes
                .GetOrAdd(objectType,
                          t =>
                          {
                              var constructorInfo = objectType
                                  .GetTypeInfo().GetConstructors(BindingFlags.Public | BindingFlags.Instance).Single();
                              var parameterInfo = constructorInfo.GetParameters().Single();
                              return parameterInfo.ParameterType;
                          });

            var value = serializer.Deserialize(reader, parameterType);
            return value != null ? Activator.CreateInstance(objectType, value) : null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var singleValueObject = value as ISingleValueObject;
            if (singleValueObject == null)
            {
                return;
            }
            serializer.Serialize(writer, singleValueObject.GetValue());
        }
    }
}