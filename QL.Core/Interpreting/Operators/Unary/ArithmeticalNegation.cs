using QL.Api.Entities;
using QL.Api.Operators;
using System;

namespace QL.Core.Interpreting.Operators
{
    internal class ArithmeticalNegation : IOperator
    {
        public string AsString
        {
            get
            {
                return "-";
            }
        }

        public Value Evaluate(Value input, Value empty = null)
        {
            if (input.Type == QLType.Integer)
            {
                return new Value(-input.ToInt(), input.Type);
            }
            else if (input.Type == QLType.Decimal)
            {
                return new Value(-input.ToDecimal(), input.Type);
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
