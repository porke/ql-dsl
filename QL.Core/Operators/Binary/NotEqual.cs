using QL.Core.Types;
using System;

namespace QL.Core.Operators
{
    internal class NotEqual : AbsoluteComparison
    {
        public override Value Evaluate(Value lhs, Value rhs)
        {
            switch (lhs.Type)
            {
                case QLType.Integer: return new Value(lhs.ToInt() != rhs.ToInt(), QLType.Boolean);
                case QLType.Decimal: return new Value(lhs.ToDecimal() != rhs.ToDecimal(), QLType.Boolean);
                case QLType.Boolean: return new Value(lhs.ToBoolean() != rhs.ToBoolean(), QLType.Boolean);
                case QLType.String: 
                case QLType.Date: return new Value(!String.Equals(lhs.ToString(), rhs.ToString(), StringComparison.Ordinal), QLType.Boolean);
            }
            throw new NotSupportedException($"{lhs.Type} is not supported by the '!=' operator");
        }
    }
}
