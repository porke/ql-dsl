using QL.Core.Types;
using System;

namespace QL.Core.Operators
{
    internal class AdditionOperator : IBinaryOperator
    {
        public Value Evaluate(Value lhs, Value rhs)
        {
            QLType finalType = OperatorResultTypeResolver.ResolveOperationType(lhs.Type, rhs.Type);
            if (finalType == QLType.Integer)
            {
                return new Value(lhs.ToInt() + rhs.ToInt(), finalType);
            }
            else if (finalType == QLType.Decimal)
            {
                return new Value(lhs.ToDecimal() + rhs.ToDecimal(), finalType);
            }
            else if (finalType == QLType.Money)
            {
                return new Value(lhs.ToMoney() + rhs.ToMoney(), finalType);
            }

            throw new NotSupportedException($"{finalType} is not supported by the addition operator");
        }
    }
}
