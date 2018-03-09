using QL.Api.Operators;
using System;

namespace QL.Core.Parsin
{
    internal static class OperatorFactory
    {
        // TODO: possibly replace parser with dictionary.
        public static IOperator CreateBinaryOperator(string text)
        {
            switch (text)
            {
                case "-": return new Substraction();
                case "+": return new Addition();
                case "*": return new Multiplication();
                case "/": return new Division();
                case "||": return new Or();
                case "&&": return new And();
                case ">": return new Greater();
                case "<": return new Smaller();
                case ">=": return new GreaterOrEqual();
                case "<=": return new SmallerOrEqual();
                case "!=": return new NotEqual();
                case "==": return new Equal();
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
