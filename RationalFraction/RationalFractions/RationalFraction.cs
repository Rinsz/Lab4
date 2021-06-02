using System.Numerics;
using System.Text.RegularExpressions;
using Lab4.Exceptions;

namespace Lab4.RationalFractions
{
    public partial struct RationalFraction
    {
        private const string FractionRegexPattern = @"-*\d+[\/]-*\d+";

        public BigInteger Numerator { get; private set; }
        public BigInteger Denominator { get; private set; }
        
        public bool IsZero => Denominator.IsZero;
        
        public bool IsOne => BigInteger.Abs(Numerator) == BigInteger.Abs(Denominator);

        public int Sign => Numerator.Sign * Denominator.Sign >= 0 ? 1 : -1;

        public static readonly RationalFraction Zero = new (0, 1);
        
        public static readonly RationalFraction One = new (1, 1);
        
        public RationalFraction(BigInteger numerator, BigInteger denominator)
        {
            if (denominator == 0)
                throw new DivideByZeroException("Denominator can not be zero");
            if (numerator < 0 && denominator < 0 || denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            Numerator = numerator;
            Denominator = denominator;
        }

        public RationalFraction(BigInteger whole)
            : this(whole, 1)
        {
        }

        public override string ToString() =>
            $"{Numerator}/{Denominator}";

        public static bool TryParse(string str, out RationalFraction fraction)
        {
            if (!Regex.IsMatch(str, FractionRegexPattern))
            {
                fraction = Zero;
                return false;
            }
            
            var parts = str.Split('\\', '/');
            fraction = new RationalFraction(BigInteger.Parse(parts[0]), BigInteger.Parse(parts[1]));
            return true;
        }
    }
}