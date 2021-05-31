using System.Numerics;

namespace Lab4.RationalFractions.Logics
{
    public static class EuclidGcd
    {
        public static BigInteger FindGcd(BigInteger first, BigInteger second)
        {
            var min = BigInteger.Min(first, second);
            var max = min == first ? second : first;

            var remainder = BigInteger.Remainder(max, min);
            if (remainder == BigInteger.Zero)
                return min;

            while (remainder != BigInteger.Zero)
            {
                max = min;
                min = remainder;
                remainder = BigInteger.Remainder(max, min);
            }
            
            return min;
        }

        public static BigInteger FindGcd(this RationalFraction fraction) =>
            FindGcd(fraction.Numerator, fraction.Denominator);
    }
}