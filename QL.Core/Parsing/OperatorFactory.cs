using QL.Api.Operators;
using QL.Api.Factories;
using System;
using Antlr4.Runtime;
using QL.Core.Interpreting.Operators.Binary;
using QL.Core.Interpreting.Operators.Unary;

namespace QL.Core.Parsing
{
    internal class OperatorFactory : IOperatorFactory
    {
        private readonly IValueFactory _valueFactory;

        internal OperatorFactory(IValueFactory valueFactory)
        {
            _valueFactory = valueFactory;
        }

        public IOperator CreateBinaryOperator(IToken token)
        {
            switch (token.Type)
            {
                case QLParser.MINUS: return new Arithmetic((x, y) => x - y, token.Text, _valueFactory);
                case QLParser.PLUS: return new Arithmetic((x, y) => x + y, token.Text, _valueFactory);
                case QLParser.MULTIPLY: return new Arithmetic((x, y) => x * y, token.Text, _valueFactory);
                case QLParser.DIVIDE: return new Arithmetic((x, y) => x / y, token.Text, _valueFactory);
                case QLParser.OR: return new Logical((x, y) => x || y, token.Text, _valueFactory);
                case QLParser.AND: return new Logical((x, y) => x && y, token.Text, _valueFactory);
                case QLParser.GREATERTHAN: return new RelativeComparison((x, y) => x > y, token.Text, _valueFactory);
                case QLParser.SMALLERTHAN: return new RelativeComparison((x, y) => x < y, token.Text, _valueFactory);
                case QLParser.GREATEREQUAL: return new RelativeComparison((x, y) => x >= y, token.Text, _valueFactory);
                case QLParser.SMALLEREQUAL: return new RelativeComparison((x, y) => x <= y, token.Text, _valueFactory);
                case QLParser.NOTEQUAL: return new AbsoluteComparison((x, y) => x != y, token.Text, _valueFactory);
                case QLParser.EQUAL: return new AbsoluteComparison((x, y) => x == y, token.Text, _valueFactory);
            }
            throw new NotSupportedException($"A token is not implemented as an operator.");
        }

        public IOperator CreateUnaryOperator(IToken token)
        {
            switch (token.Type)
            {
                case QLParser.MINUS: return new ArithmeticalNegation(token.Text, _valueFactory);
                case QLParser.NOT: return new BooleanNegation(token.Text, _valueFactory);
            }
            throw new NotSupportedException($"A token is not implemented as an operator.");
        }
    }
}
