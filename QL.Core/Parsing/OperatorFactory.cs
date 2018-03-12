using QL.Api.Operators;
using QL.Core.Interpreting.Operators;
using System;

namespace QL.Core.Parsing
{
    internal static class OperatorFactory
    {
        public static IOperator CreateBinaryOperator(string text)
        {
            switch (text)
            {
                case "-": return new Arithmetic((x, y) => x - y, "-");
                case "+": return new Arithmetic((x, y) => x + y, "+");
                case "*": return new Arithmetic((x, y) => x * y, "*");
                case "/": return new Arithmetic((x, y) => x / y, "-");
                case "||": return new Logical((x, y) => x || y, "||");
                case "&&": return new Logical((x, y) => x && y, "&&");
                case ">": return new RelativeComparison((x, y) => x > y, ">");
                case "<": return new RelativeComparison((x, y) => x < y, "<");
                case ">=": return new RelativeComparison((x, y) => x >= y, ">=");
                case "<=": return new RelativeComparison((x, y) => x <= y, "<=");
                case "!=": return new AbsoluteComparison((x, y) => x != y, "!=");
                case "==": return new AbsoluteComparison((x, y) => x == y, "==");
            }
            throw new NotSupportedException($"{text} is not implemented as an operator.");
        }

        public static IOperator CreateUnaryOperator(string text)
        {
            switch (text)
            {
                case "-": return new ArithmeticalNegation();
                case "!": return new BooleanNegation();
            }
            throw new NotSupportedException($"{text} is not implemented as an operator.");
        }
    }
}
