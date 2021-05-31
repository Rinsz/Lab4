using System.Collections.Generic;
using FluentAssertions;
using Lab4.PeriodicFractions;
using Lab4.RationalFractions;
using NUnit.Framework;

namespace Tests
{
    public class PeriodicFractionTests
    {
        [TestCaseSource(nameof(GetTestSource))]
        public void ShouldConvertToRationalFractionCorrectly((PeriodicFraction periodicFraction, RationalFraction expected) source)
        {
            source.periodicFraction.ConvertToRational().Should().Be(source.expected);
        }

        private static IEnumerable<(PeriodicFraction periodicFraction, RationalFraction expected)> GetTestSource()
        {
            yield return (new PeriodicFraction(0, "", ""), RationalFraction.Zero);
            yield return (new PeriodicFraction(1, "", ""), RationalFraction.One);
            yield return (new PeriodicFraction(2, "", ""), new RationalFraction(2));
            yield return (new PeriodicFraction(0, "25", ""), new RationalFraction(1,4));
            yield return (new PeriodicFraction(0, "345", ""), new RationalFraction(69,200));
            yield return (new PeriodicFraction(0, "", "3"), new RationalFraction(1,3));
            yield return (new PeriodicFraction(3, "", "142857"), new RationalFraction(22,7));
            yield return (new PeriodicFraction(0, "12", "7"), new RationalFraction(23,180));
            yield return (new PeriodicFraction(0, "58", "3"), new RationalFraction(7,12));
            yield return (new PeriodicFraction(0, "", "81308411214953271028037383177570093457943925233644859"),
                new RationalFraction(87,107));
        }
    }
}