using System.Numerics;
using Lab4.RationalFractions;

namespace Lab4.PeriodicFractions
{
    public static class PeriodicFractionExtensions
    {
        public static RationalFraction ConvertToRational(this PeriodicFraction fraction)
        {
            if (string.IsNullOrEmpty(fraction.PeriodicPart) && string.IsNullOrEmpty(fraction.NonPeriodicPart))
                return new RationalFraction(fraction.WholePart);
            
            if (string.IsNullOrEmpty(fraction.PeriodicPart) && !string.IsNullOrEmpty(fraction.NonPeriodicPart))
            {
                var converted = new RationalFraction(BigInteger.Parse(fraction.NonPeriodicPart),
                    BigInteger.Parse($"1{new string('0', fraction.NonPeriodicPart.Length)}"));

                return converted.ToIrreducibleFraction() + fraction.WholePart;
            }
            
            if (string.IsNullOrEmpty(fraction.NonPeriodicPart))
            {
                var converted = new RationalFraction(BigInteger.Parse(fraction.PeriodicPart),
                    BigInteger.Parse(new string('9', fraction.PeriodicPart.Length))) + fraction.WholePart;
                return converted.ToIrreducibleFraction();
            }

            var numerator = BigInteger.Parse(fraction.NonPeriodicPart + fraction.PeriodicPart) 
                            - BigInteger.Parse(fraction.NonPeriodicPart);
            var denominator = BigInteger.Parse(new string('9', fraction.PeriodicPart.Length) 
                                               + new string('0', fraction.NonPeriodicPart.Length));

            return new RationalFraction(numerator, denominator).ToIrreducibleFraction() + fraction.WholePart;
        }
    }
}