using System;

namespace Lab4.Exceptions
{
    public class DivideByZeroException : Exception
    {
        public DivideByZeroException()
        {
        }

        public DivideByZeroException(string error) : base(error)
        {
        }
    }
}