using System;
using EventFlow.ValueObjects;

namespace PizzaMaster.Konten.ValueObjects
{
    public class Benutzer : SingleValueObject<string>
    {
        public Benutzer(string value) : base(ValidateAndConvert(value)) { }

        private static string ValidateAndConvert(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
            return value.ToLower();
        }
    }
}