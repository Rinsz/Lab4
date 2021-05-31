using System.Numerics;

namespace Lab4.RationalFractions
{
    public partial struct RationalFraction
    {
        public static implicit operator RationalFraction(sbyte n) => n != 0 ? new RationalFraction(n) : Zero;
        
        public static implicit operator RationalFraction(byte n) => n != 0 ? new RationalFraction(n) : Zero;
        
        public static implicit operator RationalFraction(short n) => n != 0 ? new RationalFraction(n) : Zero;
        
        public static implicit operator RationalFraction(ushort n) => n != 0 ? new RationalFraction(n) : Zero;
        
        public static implicit operator RationalFraction(int n) => n != 0 ? new RationalFraction(n) : Zero;
        
        public static implicit operator RationalFraction(uint n) => n != 0 ? new RationalFraction(n) : Zero;
        
        public static implicit operator RationalFraction(long n) => n != 0 ? new RationalFraction(n) : Zero;
        
        public static implicit operator RationalFraction(ulong n) => n != 0 ? new RationalFraction(n) : Zero;
        
        public static implicit operator RationalFraction(BigInteger n) => !n.IsZero ? new RationalFraction(n) : Zero;
    }
}