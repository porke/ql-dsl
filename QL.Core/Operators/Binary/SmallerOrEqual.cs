using QL.Core.Types;
using System;

namespace QL.Core.Operators
{
    internal class SmallerOrEqual : RelativeComparison
    {
        public override Value Evaluate(Value leftHand, Value rightHand)
        {
            if (leftHand.Type == rightHand.Type)
            {
                switch (leftHand.Type)
                {
                    case QLType.Integer: return new Value(leftHand.ToInt() <= rightHand.ToInt(), QLType.Boolean);
                    case QLType.Decimal: return new Value(leftHand.ToDecimal() <= rightHand.ToDecimal(), QLType.Boolean);
                    case QLType.Date: return new Value(true, QLType.Boolean); // TODO: Implement date comparison.
                }
            }
            return new Value(leftHand.ToDecimal() <= rightHand.ToDecimal(), QLType.Boolean);
        }
    }
}
