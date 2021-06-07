using System;
using System.Numerics;
using System.Text.RegularExpressions;
using Lab4.PeriodicFractions;
using Lab4.RationalFractions;
using Lab4.RationalFractions.Extensions;

namespace ConsoleInterface
{
    class Program
    {
        private const string FractionExpressionRegexPattern = "^-*\\d+[\\/\\\\]-*\\d+ [-+\\*\\/] -*\\d+[\\/\\\\]-*\\d+$";
        private const string PeriodicFractionRegexPattern = @"^-?\d+[.,]\d*(\(\d+\))?$";
        private const string DecimalFractionRegexPattern = @"^-?\d+[.,]\d+$";
        private const string SimplePeriodicFractionRegexPattern = @"^-?\d+[.,](\(\d+\))$";

        static void Main(string[] args)
        {
            Console.WriteLine("To see all available commands type 'help'...");
            var cmd = "default";
            while (!string.IsNullOrEmpty(cmd))
            {
                Console.WriteLine("\nEnter command or expression: ");
                cmd = Console.ReadLine()?.Trim();
                
                if (Regex.IsMatch(cmd, FractionExpressionRegexPattern))
                {
                    SolveExpression(cmd);
                    continue;
                }
                
                switch (cmd)
                {
                    case "rational-to-periodic": ConvertRationalToPeriodic(); break;
                    case "periodic-to-rational": ConvertPeriodicToRational(); break;
                    case "help": Console.WriteLine("'rational-to-periodic' - converts rational fraction to periodic\n" +
                                                   "'periodic-to-rational' - converts periodic or decimal fraction to rational\n" +
                                                   "expression in format x/y + a/b. Separating whitespaces are necessary"); break;
                    default: Console.WriteLine("Wrong command. Type 'help' to see all available commands."); break;
                }
            }
        }

        private static void ConvertPeriodicToRational()
        {
            Console.WriteLine("Enter periodic fraction: ");
            var fraction = Console.ReadLine()?.Trim();
            if (!Regex.IsMatch(fraction, PeriodicFractionRegexPattern))
            {
                Console.WriteLine("Fraction does not match pattern");
                Console.WriteLine("Availible patterns:\nPeriodic: x.(y)\nDecimal: x.y\nPeriodic with decimal x.y(z)");
                return;
            }

            var parts = fraction.Split('.',',');
            var whole = parts[0];
            var nonPeriodic = null as string;
            var periodic = null as string;

            if (Regex.IsMatch(fraction, DecimalFractionRegexPattern))
            {
                nonPeriodic = parts[1];
                var frac = new PeriodicFraction(BigInteger.Parse(whole), nonPeriodic, periodic,
                    whole[0] == '-' ? -1 : 1);
                Console.WriteLine(frac.ConvertToRational());
                return;
            }

            if (Regex.IsMatch(fraction, SimplePeriodicFractionRegexPattern))
            {
                periodic = parts[1].Trim('(', ')');
                var frac = new PeriodicFraction(BigInteger.Parse(whole), nonPeriodic, periodic,
                    whole[0] == '-' ? -1 : 1);
                Console.WriteLine(frac.ConvertToRational());
                return;
            }

            var fractionalParts = parts[1].Split('(');
            nonPeriodic = fractionalParts[0];
            periodic = fractionalParts[1].Trim('(', ')');
            var periodicFraction =
                new PeriodicFraction(BigInteger.Parse(whole), nonPeriodic, periodic, whole[0] == '-' ? -1 : 1);
            Console.WriteLine(periodicFraction.ConvertToRational());
        }

        private static void ConvertRationalToPeriodic()
        {
            Console.WriteLine("Enter fraction to convert: ");
            var frac = Console.ReadLine();
            Console.WriteLine("Enter amount of signs after the comma");
            var signs = int.Parse(Console.ReadLine());
            
            if(RationalFraction.TryParse(frac, out var fraction))
                Console.WriteLine(fraction.ToPeriodicFraction(signs));
            else
                throw new Exception("Wrong fraction");
        }

        private static void SolveExpression(string cmd)
        {
            cmd = cmd.Trim();
            var expressionParts = cmd.Split(' ');
            if (!RationalFraction.TryParse(expressionParts[0], out var leftArgument))
                throw new Exception("Wrong left argument");
            if (!RationalFraction.TryParse(expressionParts[2], out var rightArgument))
                throw new Exception("Wrong right argument");

            switch (expressionParts[1])
            {
                case "+" : Console.WriteLine($"{leftArgument + rightArgument}\n"); break;
                case "-" : Console.WriteLine($"{leftArgument - rightArgument}\n"); break;
                case "/" : Console.WriteLine($"{leftArgument / rightArgument}\n"); break;
                case "*" : Console.WriteLine($"{leftArgument * rightArgument}\n"); break;
                default: throw new Exception("Wrong operation");
            }
        }
    }
}