using QL.Core.Types;
using System;

namespace QL.Core.Operators
{
    internal class NotEqual : AbsoluteComparison
    {
        public override Value Evaluate(Value leftHand, Value rightHand)
        {
            switch (leftHand.Type)
            {
                case QLType.Integer: return new Value(leftHand.ToInt() != rightHand.ToInt(), QLType.Boolean);
                case QLType.Decimal: return new Value(leftHand.ToDecimal() != rightHand.ToDecimal(), QLType.Boolean);
                case QLType.Boolean: return new Value(leftHand.ToBoolean() != rightHand.ToBoolean(), QLType.Boolean);
                case QLType.String: 
                case QLType.Date: return new Value(!string.Equals(leftHand.ToString(), rightHand.ToString(), StringComparison.Ordinal), QLType.Boolean);
            }
            throw new NotSupportedException($"{leftHand.Type} is not supported by the '!=' operator");
        }
    }
}
