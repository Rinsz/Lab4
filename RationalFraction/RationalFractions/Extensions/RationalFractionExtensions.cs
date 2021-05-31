using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Lab4.PeriodicFractions;

namespace Lab4.RationalFractions.Extensions
{
    public static class RationalFractionExtensions
    {
        public static PeriodicFraction ToPeriodicFraction(this RationalFraction fraction, int fractionPartMaxLength = 100)
        {
            if (fraction.IsZero)
                return PeriodicFraction.Zero;

            if (fraction.IsOne)
                return fraction.Sign > 0 ? PeriodicFraction.WholeOne : PeriodicFraction.NegativeWholeOne;

            fraction = fraction.ToIrreducibleFraction();

            var divisor = BigInteger.Abs(fraction.Denominator);
            var wholePart = BigInteger.DivRem(
                BigInteger.Abs(fraction.Numerator), 
                divisor,
                out var remainder);

            if (remainder == 0)
                return new PeriodicFraction(wholePart, "", "");
            
            var (nonPeriodicalPart, periodicalPart) = GetFractionalParts(
                remainder,
                divisor,
                fractionPartMaxLength);
            
            return new PeriodicFraction(wholePart, nonPeriodicalPart, periodicalPart, fraction.Sign);
        }

        private static (string nonPeriodicalPart, string periodicalPart) GetFractionalParts(
            BigInteger remainder, BigInteger divisor, int fractionPartMaxLength)
        {
            var previousRemainders = new List<BigInteger>();
            var sb = new StringBuilder();
            
            while (remainder != BigInteger.Zero 
                   && sb.Length <= fractionPartMaxLength 
                   && !previousRemainders.Contains(remainder))
            {
                previousRemainders.Add(remainder);
                
                remainder *= 10;
                if (remainder < divisor)
                {
                    sb.Append(0);
                    continue;
                }

                sb.Append(BigInteger.DivRem(remainder, divisor, out remainder));
            }

            var fractionPart = sb.ToString();
            if (!previousRemainders.Contains(remainder))
                return (fractionPart, "");

            var periodStartIndex = previousRemainders.IndexOf(remainder);
            var periodicalPart = fractionPart.Substring(
                periodStartIndex,
                previousRemainders.Count - periodStartIndex);

            fractionPart = fractionPart.Remove(periodStartIndex, previousRemainders.Count - periodStartIndex);
            
            return (fractionPart, periodicalPart);
        }
    }
}