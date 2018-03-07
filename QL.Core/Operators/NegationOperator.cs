using QL.Core.Types;
using System;

namespace QL.Core.Operators
{
    internal class NegationOperator : IUnaryOperator
    {
        public Value Evaluate(Value input)
        {
            if (input.Type == QLType.Integer)
            {
                return new Value(-input.ToInt(), input.Type);
            }
            else if (input.Type == QLType.Decimal)
            {
                return new Value(-input.ToDecimal(), input.Type);
            }

            throw new NotSupportedException($"{input.Type} is not supported by the negation operator.");
        }
    }
}
