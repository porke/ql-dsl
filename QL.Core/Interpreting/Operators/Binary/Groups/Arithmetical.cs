using System;
using QL.Api.Operators;
using QL.Api.Types;

namespace QL.Core.Interpreting.Operators
{
    internal abstract class Arithmetical : IOperator
    {
        public bool AcceptTypes(QLType leftHand, QLType rightHand)
        {
            return ((leftHand == QLType.Integer || leftHand == QLType.Decimal)
                && (rightHand == QLType.Integer || rightHand == QLType.Decimal));
        }

        public abstract Value Evaluate(Value leftInput, Value rightInput = null);

        public QLType ResultingType(QLType leftType, QLType rightType)
        {
            if (leftType == rightType)
            {
                return leftType;
            }
            return QLType.Decimal;
        }
    }
}
