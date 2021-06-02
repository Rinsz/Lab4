using System.Numerics;

namespace Lab4.PeriodicFractions
{
    public struct PeriodicFraction
    {
        public BigInteger WholePart { get; private set; }
        public string NonPeriodicPart { get; private set; }
        public string PeriodicPart { get; private set; }
        
        private int sign;

        public PeriodicFraction(BigInteger wholePart, string nonPeriodicPart, string periodicPart, int sign = 1)
        {
            WholePart = wholePart;
            NonPeriodicPart = nonPeriodicPart;
            PeriodicPart = periodicPart;
            this.sign = wholePart > 0 && sign == 1 ? 1 : 0;
        }

        public static PeriodicFraction Zero => 
            new (0, "", "");

        public static PeriodicFraction WholeOne =>
            new(1, "", "");
        
        public static PeriodicFraction NegativeWholeOne =>
            new(-1, "", "");

        public override string ToString()
        {
            if (string.IsNullOrEmpty(PeriodicPart) && string.IsNullOrEmpty(NonPeriodicPart))
                return WholePart.ToString();
            return $"{(WholePart == 0 && sign == -1 ? "-" : "")}" +
                   $"{WholePart}.{NonPeriodicPart}" + (string.IsNullOrEmpty(PeriodicPart) ? "" : $"({PeriodicPart})");
        } 
        
    }
}