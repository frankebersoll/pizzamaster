using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.ValueObjects;

namespace PizzaMaster.Domain.Common
{
    public class Benutzer : SingleValueObject<string>
    {
        public Benutzer(string value) : base(ValidateAndConvert(value)) { }

        public override string ToString()
        {
            return string.Join(" ", this.Value
                                        .Split(' ')
                                        .Select(token => char.ToUpper(token[0]) + token.Substring(1)));
        }

        private static string ValidateAndConvert(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
            return value.ToLower();
        }
    }
}