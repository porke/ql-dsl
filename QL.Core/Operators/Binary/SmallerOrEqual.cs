using QL.Core.Types;
using System;

namespace QL.Core.Operators
{
    internal class SmallerOrEqual : RelativeComparison
    {
        public override Value Evaluate(Value lhs, Value rhs)
        {
            QLType finalType = ResultTypeResolver.ResolveOperationType(lhs.Type, rhs.Type);
            switch (finalType)
            {
                case QLType.Integer: return new Value(lhs.ToInt() <= rhs.ToInt(), QLType.Boolean);
                case QLType.Decimal: return new Value(lhs.ToDecimal() <= rhs.ToDecimal(), QLType.Boolean);
                case QLType.Date: return new Value(true, QLType.Boolean); // TODO: Implement date comparison.
            }
            throw new NotSupportedException($"{finalType} is not supported by the '<=' operator");
        }
    }
}
