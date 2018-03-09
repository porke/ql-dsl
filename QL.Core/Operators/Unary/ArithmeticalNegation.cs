using QL.Api.Operators;
using QL.Api.Types;
using System;

namespace QL.Core.Operators
{
    internal class ArithmeticalNegation : Operator
    {
        public override Value Evaluate(Value input, Value empty = null)
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

        public override bool AcceptTypes(QLType value, QLType empty = QLType.Undefined)
        {
            return (value == QLType.Integer || value == QLType.Decimal);
        }

        public override QLType ResultingType(QLType value, QLType empty = QLType.Undefined)
        {
            return value;
        }
    }
}
