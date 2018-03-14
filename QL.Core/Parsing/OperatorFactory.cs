using QL.Api.Operators;
using QL.Core.Interpreting.Operators;
using QL.Api.Factories;
using System;

namespace QL.Core.Parsing
{
    internal class OperatorFactory : IOperatorFactory
    {
        private readonly IValueFactory _valueFactory;

        internal OperatorFactory(IValueFactory valueFactory)
        {
            _valueFactory = valueFactory;
        }

        public IOperator CreateBinaryOperator(string text)
        {
            switch (text)
            {
                case "-": return new Arithmetic((x, y) => x - y, "-", _valueFactory);
                case "+": return new Arithmetic((x, y) => x + y, "+", _valueFactory);
                case "*": return new Arithmetic((x, y) => x * y, "*", _valueFactory);
                case "/": return new Arithmetic((x, y) => x / y, "-", _valueFactory);
                case "||": return new Logical((x, y) => x || y, "||", _valueFactory);
                case "&&": return new Logical((x, y) => x && y, "&&", _valueFactory);
                case ">": return new RelativeComparison((x, y) => x > y, ">", _valueFactory);
                case "<": return new RelativeComparison((x, y) => x < y, "<", _valueFactory);
                case ">=": return new RelativeComparison((x, y) => x >= y, ">=", _valueFactory);
                case "<=": return new RelativeComparison((x, y) => x <= y, "<=", _valueFactory);
                case "!=": return new AbsoluteComparison((x, y) => x != y, "!=", _valueFactory);
                case "==": return new AbsoluteComparison((x, y) => x == y, "==", _valueFactory);
            }
            throw new NotSupportedException($"{text} is not implemented as an operator.");
        }

        public IOperator CreateUnaryOperator(string text)
        {
            switch (text)
            {
                case "-": return new ArithmeticalNegation(_valueFactory);
                case "!": return new BooleanNegation(_valueFactory);
            }
            throw new NotSupportedException($"{text} is not implemented as an operator.");
        }
    }
}
