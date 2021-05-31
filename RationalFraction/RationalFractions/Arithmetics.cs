using Lab4.Exceptions;

namespace Lab4.RationalFractions
{
    public partial struct RationalFraction
    {
        public static RationalFraction operator +(RationalFraction left, RationalFraction right)
        {
            if (right.IsZero)
                return left;

            if (left.IsZero)
                return right;

            if (left.Denominator == right.Denominator)
                return new RationalFraction(left.Numerator + right.Numerator, left.Denominator);
            
            var numerator = left.Numerator * right.Denominator + left.Denominator * right.Numerator;
            var denominator = left.Denominator * right.Denominator;
            return new RationalFraction(numerator, denominator);
        }

        public static RationalFraction operator +(RationalFraction left, int right) =>
            new (left.Numerator + left.Denominator * right, left.Denominator);
        

        public static RationalFraction operator ++(RationalFraction fraction) => fraction + One;

        public static RationalFraction operator -(RationalFraction fraction) => 
            fraction.Negate();

        public static RationalFraction operator -(RationalFraction left, RationalFraction right)
        {
            if (right.IsZero)
                return left;

            if (left.IsZero)
                return -right;

            if (left.Denominator == right.Denominator)
                return new RationalFraction(left.Numerator - right.Numerator, left.Denominator);
            
            var numerator = left.Numerator * right.Denominator - left.Denominator * right.Numerator;
            var denominator = left.Denominator * right.Denominator;
            var difference = new RationalFraction(numerator, denominator);
            return difference;
        }
        
        public static RationalFraction operator --(RationalFraction fraction) => fraction - One;

        public static RationalFraction operator *(RationalFraction left, RationalFraction right)
        {
            if (left.IsZero || right.IsZero)
                return Zero;

            var numerator = left.Numerator * right.Numerator;
            var denominator = left.Denominator * right.Denominator;
            return new RationalFraction(numerator, denominator);
        }

        public static RationalFraction operator /(RationalFraction left, RationalFraction right)
        {
            if (right.IsZero)
                throw new DivideByZeroException();

            if (left.IsZero)
                return Zero;

            var numerator = left.Numerator * right.Denominator;
            var denominator = left.Denominator * right.Numerator;
            return new RationalFraction(numerator, denominator);
        }
    }
}