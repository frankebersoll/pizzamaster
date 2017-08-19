using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.Core;
using Newtonsoft.Json;

namespace PizzaMaster.Application.Serialization
{
    public class SimpleJsonSerializer : IJsonSerializer
    {
        private static readonly JsonSerializerSettings SettingsIndented = CreateSettings(true);

        private static readonly JsonSerializerSettings SettingsNotIndented = CreateSettings();

        public object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, SettingsNotIndented);
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, SettingsNotIndented);
        }

        public string Serialize(object obj, bool indented = false)
        {
            JsonSerializerSettings settings = indented ? SettingsIndented : SettingsNotIndented;
            return JsonConvert.SerializeObject(obj, settings);
        }

        private static JsonSerializerSettings CreateSettings(bool indented = false)
            => new JsonSerializerSettings
               {
                   Formatting = indented ? Formatting.Indented : Formatting.None,
                   ContractResolver =
                       new ReadOnlyPropertyResolver(typeof(IAggregateEvent)),
                   Converters =
                   {
                       new SingleValueObjectConverter()
                   }
               };
    }
}