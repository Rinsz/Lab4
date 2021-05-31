using System.Collections.Generic;
using System.Numerics;
using FluentAssertions;
using Lab4.Exceptions;
using Lab4.PeriodicFractions;
using Lab4.RationalFractions;
using Lab4.RationalFractions.Extensions;
using Lab4.RationalFractions.Logics;
using NUnit.Framework;

namespace Tests
{
    public class RationalFractionTests
    {

        [Test]
        public void ShouldThrow_IfDenominatorIsZero() =>
            Assert.Throws<DivideByZeroException>(() => new RationalFraction(1, 0));

        #region cases

        [TestCase("15/30", "10/30", "25/30")]
        [TestCase("1020/3231", "1132/3231", "2152/3231")]
        [TestCase("1012/534331", "7346/3231", "3928465298/1726423461")]
        [TestCase("102533420/3264236231", "11342532/3254325231", "370701799632396912/10622886326487644361")]

        #endregion
        public void ShouldAddFractionsCorrectly(string firstFraction, string secondFraction, string expectedResult)
        {
            var (frac1, frac2, expectedFraction) = ParseFractions(firstFraction, secondFraction, expectedResult);

            var sum = frac1 + frac2;

            sum.Should().Be(expectedFraction);
        }

        #region cases

        [TestCase("15/30", "10/30", "5/30")]
        [TestCase("1020/3231", "1132/3231", "-112/3231")]
        [TestCase("1012/534331", "7346/3231", "-3921925754/1726423461")]
        [TestCase("102533420/3264236231", "11342532/3254325231", "296652391821043128/10622886326487644361")]

        #endregion
        [Test]
        public void ShouldSubtractFractionsCorrectly(string firstFraction, string secondFraction, string expectedResult)
        {
            var (frac1, frac2, expectedFraction) = ParseFractions(firstFraction, secondFraction, expectedResult);

            var diff = frac1 - frac2;

            diff.Should().Be(expectedFraction);
        }
        
        #region cases

        [TestCase("15/30", "10/30", "150/900")]
        [TestCase("1020/3231", "1132/3231", "1154640/10439361")]
        [TestCase("1012/534331", "7346/3231", "7434152/1726423461")]
        [TestCase("102533420/3264236231", "11342532/3254325231", "1162988597419440/10622886326487644361")]

        #endregion
        [Test]
        public void ShouldMultiplyFractionsCorrectly(string firstFraction, string secondFraction, string expectedResult)
        {
            var (frac1, frac2, expectedFraction) = ParseFractions(firstFraction, secondFraction, expectedResult);

            var result = frac1 * frac2;

            result.Should().Be(expectedFraction);
        }
        
        #region cases

        [TestCase("15/30", "10/30", "450/300")]
        [TestCase("1020/3231", "1132/3231", "3295620/3657492")]
        [TestCase("1012/534331", "7346/3231", "3269772/3925195526")]
        [TestCase("102533420/3264236231", "11342532/3254325231", "333677095726720020/37024703905676892")]

        #endregion
        [Test]
        public void ShouldDivideFractionsCorrectly(string firstFraction, string secondFraction, string expectedResult)
        {
            var (frac1, frac2, expectedFraction) = ParseFractions(firstFraction, secondFraction, expectedResult);

            var result = frac1 / frac2;

            result.Should().Be(expectedFraction);
        }

        #region cases

        [TestCase("15/30", "-15/30")]
        [TestCase("-1020/3231", "1020/3231")]
        [TestCase("1012/534331", "1012/-534331")]
        [TestCase("-102533420/-3264236231", "-102533420/3264236231")]

        #endregion
        [Test]
        public void ShouldNegateFractionsCorrectly(string sourceFraction, string expectedResult)
        {
            var parts = sourceFraction.Split('/');
            var source = new RationalFraction(BigInteger.Parse(parts[0]), BigInteger.Parse(parts[1]));
            var resParts = expectedResult.Split('/');
            var expected = new RationalFraction(BigInteger.Parse(resParts[0]), BigInteger.Parse(resParts[1]));

            source.Negate().Should().Be(expected);
        }

        [TestCase("6", "9", "2","3")]
        [TestCase("100", "500", "1","5")]
        [TestCase("47823567826", "17864782634", "23911783913", "8932391317")]
        public void ShouldReduceFractionCorrectly(string numerator, string denominator, string expectedNumerator,
            string expectedDenominator)
        {
            var reducedFraction = new RationalFraction(GetBigIntFromString(numerator), GetBigIntFromString(denominator))
                .ToIrreducibleFraction();
            reducedFraction.Numerator.Should().Be(GetBigIntFromString(expectedNumerator));
            reducedFraction.Denominator.Should().Be(GetBigIntFromString(expectedDenominator));
        }

        [TestCase("17/13", "1")]
        [TestCase("10/15", "5")]
        [TestCase("8978958/34666238", "2")]
        public void ShouldFindGcdCorrectly(string sourceFraction, string expected)
        {
            var parts = sourceFraction.Split('/');
            var fraction = new RationalFraction(BigInteger.Parse(parts[0]), BigInteger.Parse(parts[1]));

            fraction.FindGcd().Should().Be(BigInteger.Parse(expected));
        }

        [TestCaseSource(nameof(GetPeriodicTestCases))]
        public void ShouldConvertToPeriodicFractionCorrectly((RationalFraction fraction, PeriodicFraction expected) source)
        {
            source.fraction.ToPeriodicFraction(10).Should().Be(source.expected);
        }

        private static IEnumerable<(RationalFraction fraction, PeriodicFraction expected)> GetPeriodicTestCases()
        {
            yield return (new RationalFraction(1, 3), 
                new PeriodicFraction(0, "", "3"));
            yield return (new RationalFraction(7, 12), 
                new PeriodicFraction(0, "58", "3"));
            yield return (new RationalFraction(19, 12), 
                new PeriodicFraction(1, "58", "3"));
            yield return (new RationalFraction(87645782,678425678), 
                new PeriodicFraction(0, "12918995380", ""));
        }

        private static (RationalFraction frac1, RationalFraction frac2, RationalFraction expectedFraction)
            ParseFractions(string firstFraction, string secondFraction, string expectedResult)
        {
            var frac1 = firstFraction.Split('/');
            var fraction1 = new RationalFraction(BigInteger.Parse(frac1[0]), BigInteger.Parse(frac1[1]));
            var frac2 = secondFraction.Split('/');
            var fraction2 = new RationalFraction(BigInteger.Parse(frac2[0]), BigInteger.Parse(frac2[1]));
            var expectedFrac = expectedResult.Split('/');
            var expectedFraction =
                new RationalFraction(BigInteger.Parse(expectedFrac[0]), BigInteger.Parse(expectedFrac[1]));

            return (fraction1, fraction2, expectedFraction);
        }
        
        private static BigInteger GetBigIntFromString(string value) => BigInteger.Parse(value);
    }
}