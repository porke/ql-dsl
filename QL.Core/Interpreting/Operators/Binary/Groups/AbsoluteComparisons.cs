using QL.Api.Operators;
using QL.Api.Types;

namespace QL.Core.Interpreting.Operators
{
    internal abstract class AbsoluteComparison : IOperator
    {
        public bool AcceptTypes(QLType leftHand, QLType rightHand)
        {
            if ((leftHand == QLType.Integer || leftHand == QLType.Decimal)
                && (rightHand == QLType.Integer || rightHand == QLType.Decimal))
            {
                return true;
            }
            return (leftHand == rightHand);
        }

        public QLType ResultingType(QLType leftType, QLType rightType) {
            return QLType.Boolean;
        }

        public abstract Value Evaluate(Value leftInput, Value rightInput = null);
    }
}
