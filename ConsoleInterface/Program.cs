using System;
using System.Text.RegularExpressions;
using Lab4.RationalFractions;
using Lab4.RationalFractions.Extensions;

namespace ConsoleInterface
{
    class Program
    {
        private const string FractionExpressionRegexPattern = "^-*\\d+[\\/\\\\]-*\\d+ [-+\\*\\/] -*\\d+[\\/\\\\]-*\\d+$";

        static void Main(string[] args)
        {
            Console.WriteLine("To see all available commands type 'help'...");
            var cmd = "default";
            while (!string.IsNullOrEmpty(cmd))
            {
                Console.WriteLine("Enter command: ");
                cmd = Console.ReadLine()?.Trim();
                
                if (Regex.IsMatch(cmd, FractionExpressionRegexPattern))
                {
                    SolveExpression(cmd);
                    continue;
                }
                
                switch (cmd)
                {
                    case "rational-to-periodic": ConvertRationalToPeriodic(); break;
                    default: Console.WriteLine("Wrong command. Type 'help' to see all available commands.");
                        break;
                }
            }
        }

        private static void ConvertRationalToPeriodic()
        {
            Console.WriteLine("Enter fraction to convert: ");
            var frac = Console.ReadLine();
            if(RationalFraction.TryParse(frac, out var fraction))
                Console.WriteLine(fraction.ToPeriodicFraction());
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
                case "+" : Console.WriteLine(leftArgument + rightArgument); break;
                case "-" : Console.WriteLine(leftArgument - rightArgument); break;
                case "/" : Console.WriteLine(leftArgument / rightArgument); break;
                case "*" : Console.WriteLine(leftArgument * rightArgument); break;
                default: throw new Exception("Wrong operation");
            }
        }
    }
}