using QL.Core.Types;
using System;

namespace QL.Core.Operators
{
    internal class Substraction : Arithmetical
    {
        public override Value Evaluate(Value leftHand, Value rightHand)
        {
            QLType finalType = ResultingType(leftHand.Type, rightHand.Type);
            if (finalType == QLType.Integer)
            {
                return new Value(leftHand.ToInt() - rightHand.ToInt(), finalType);
            }
            else if (finalType == QLType.Decimal)
            {
                return new Value(leftHand.ToDecimal() - rightHand.ToDecimal(), finalType);
            }
            throw new NotSupportedException($"{finalType} is not supported by the '-' operator");
        }
    }
}
