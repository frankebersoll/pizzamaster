using System;
using EventFlow.ValueObjects;

namespace PizzaMaster.Domain.Common
{
    public class Betrag : SingleValueObject<decimal>
    {
        public Betrag(decimal value) : base(value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("Betrag muss größer gleich 0 sein.");
        }

        public static implicit operator Betrag(decimal value)
        {
            return new Betrag(value);
        }

        public static implicit operator decimal(Betrag betrag)
        {
            return betrag.Value;
        }
    }
}