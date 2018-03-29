using System;
using QL.Api.Entities;
using QL.Api.Factories;
using QL.Api.Operators;

namespace QL.Core.Interpreting.Operators.Unary
{
    internal class ArithmeticalNegation : IOperator
    {
        private string _asString;
        private readonly IValueFactory _valueFactory;

        internal ArithmeticalNegation(string asString, IValueFactory valueFactory)
        {
            _asString = asString;
            _valueFactory = valueFactory;
        }

        public override string ToString()
        {
            return _asString;
        }

        public IValue Evaluate(IValue input, IValue empty = null)
        {
            if (input.Type == QLType.Integer)
            {
                return _valueFactory.CreateValue(-input.ToInt(), input.Type);
            }
            else if (input.Type == QLType.Decimal)
            {
                return _valueFactory.CreateValue(-input.ToDecimal(), input.Type);
            }

            throw new NotSupportedException($"{input.Type} is not supported by the '-' operator.");
        }

        public bool AcceptTypes(QLType value, QLType empty = QLType.Undefined)
        {
            return (value == QLType.Integer || value == QLType.Decimal);
        }

        public QLType ResultingType(QLType value, QLType empty = QLType.Undefined)
        {
            return value;
        }
    }
}
