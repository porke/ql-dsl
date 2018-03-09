using System;
using QL.Api.Operators;
using QL.Api.Types;

namespace QL.Core.Interpreting.Operators
{
    internal abstract class Logical : IOperator
    {
        public bool AcceptTypes(QLType leftHand, QLType rightHand)
        {
            return (leftHand == QLType.Boolean && rightHand == QLType.Boolean);
        }

        public abstract Value Evaluate(Value leftInput, Value rightInput = null);

        public QLType ResultingType(QLType leftType, QLType rightType)
        {
            return QLType.Boolean;
        }
    }
}
