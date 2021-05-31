using System.Numerics;
using Lab4.RationalFractions.Logics;

namespace Lab4.RationalFractions
{
    public static class RationalFractionOperations
    {
        public static RationalFraction Negate(this RationalFraction fraction)
        {
            if (fraction.IsZero)
                return RationalFraction.Zero;

            BigInteger numerator;
            BigInteger denominator;
            if (fraction.Denominator < 0)
            {
                numerator = fraction.Numerator;
                denominator = BigInteger.Negate(fraction.Denominator);
            }
            else
            {
                numerator = BigInteger.Negate(fraction.Numerator);
                denominator = fraction.Denominator;
            }
            var result = new RationalFraction(numerator, denominator);
            return result;
        }
        
        public static RationalFraction Invert(this RationalFraction fraction) =>
            new (fraction.Denominator, fraction.Numerator);

        public static RationalFraction ToIrreducibleFraction(this RationalFraction fraction)
        {
            var gcd = fraction.FindGcd();
            return new RationalFraction(fraction.Numerator / gcd, fraction.Denominator / gcd);
        }
    }
}