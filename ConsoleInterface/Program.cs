using System;
using System.Numerics;
using Lab4.PeriodicFractions;
using Lab4.RationalFractions;
using Lab4.RationalFractions.Extensions;

namespace ConsoleInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter command: ");
            var command = Console.ReadLine();

            while (!string.IsNullOrEmpty(command))
            {
                switch (command)
                {
                    case "help" : break;
                    case "rational-to-periodic" : RationalToPeriodic(); break;
                    case "periodic-to-rational" : PeriodicToRational(); break;
                    case "rational-calculator" : Calculator(); break;
                    default: Console.WriteLine("Invalid command. Enter 'help' to see availiable commands"); break;
                }
            }
            var a = new RationalFraction(BigInteger.Parse("87"), 
                BigInteger.Parse("107"));
            var periodic = a.ToPeriodicFraction(100);
            Console.WriteLine(periodic);
            var rational = periodic.ConvertToRational();
            Console.WriteLine($"Rational: {rational}");
        }

        private static void Calculator()
        {
            Console.WriteLine("Enter first rational fraction in format 'numerator/denominator'");
            throw new NotImplementedException();
        }

        private static void PeriodicToRational()
        {
            Console.WriteLine("Enter periodic fraction in format '1.23(456)': ");
            var source = Console.ReadLine();
            if (string.IsNullOrEmpty(source))
                return;
            var parts = source.Split('.');
            var whole = parts[0];
            if(parts.Length == 1)
                Console.WriteLine(new PeriodicFraction(BigInteger.Parse(whole), "", "").ConvertToRational());
            var fractParts = parts[1].Split('(', ')');

        }

        private static void RationalToPeriodic()
        {
            Console.WriteLine("Enter rational fraction in format 'numerator/denominator': ");
            var source = Console.ReadLine();
            if (string.IsNullOrEmpty(source))
                return;
            var parts = source.Split('/');

            if (parts.Length != 2)
            {
                Console.WriteLine("Wrong format");
                return;
            }
            
            Console.WriteLine(
                $"Periodic or decimal value: {new RationalFraction(BigInteger.Parse(parts[0]), BigInteger.Parse(parts[1])).ToPeriodicFraction()}");
        }
    }
}