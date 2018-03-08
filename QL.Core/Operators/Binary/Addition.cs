using QL.Core.Types;
using System;

namespace QL.Core.Operators
{
    internal class Addition : Arithmetical
    {
        public override Value Evaluate(Value lhs, Value rhs)
        {
            QLType finalType = ResultTypeResolver.ResolveOperationType(lhs.Type, rhs.Type);
            if (finalType == QLType.Integer)
            {
                return new Value(lhs.ToInt() + rhs.ToInt(), finalType);
            }
            else if (finalType == QLType.Decimal)
            {
                return new Value(lhs.ToDecimal() + rhs.ToDecimal(), finalType);
            }
            throw new NotSupportedException($"{finalType} is not supported by the '+' operator");
        }
    }
}
