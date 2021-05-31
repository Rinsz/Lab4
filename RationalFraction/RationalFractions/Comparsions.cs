using System.Numerics;

namespace Lab4.RationalFractions
{
    public partial struct RationalFraction
    {
        public override bool Equals(object obj)
        {
            if (obj is RationalFraction rationalFraction)
                return Numerator == rationalFraction.Numerator && Denominator == rationalFraction.Denominator;
            return base.Equals(obj);
        }
        
        public static bool operator ==(RationalFraction left, RationalFraction right) => left.Equals(right);
        
        public static bool operator !=(RationalFraction left, RationalFraction right) => !left.Equals(right);
        
        public static bool operator <(RationalFraction left, RationalFraction right) => left.CompareTo(right) < 0;
        
        public static bool operator >(RationalFraction left, RationalFraction right) => left.CompareTo(right) > 0;
        
        public static bool operator <=(RationalFraction left, RationalFraction right) => left.CompareTo(right) <= 0;
        
        public static bool operator >=(RationalFraction left, RationalFraction right) => left.CompareTo(right) >= 0;

        public int CompareTo(RationalFraction other)
        {
            if (Sign == other.Sign)
            {
                if (Sign >= 0)
                    return (Numerator * other.Denominator).CompareTo(other.Numerator * Denominator);

                return
                    -BigInteger.Abs(Numerator * other.Denominator)
                        .CompareTo(BigInteger.Abs(other.Numerator * Denominator));
            }

            if (Sign > other.Sign)
                return 1;

            return -1;
        }
    }
}