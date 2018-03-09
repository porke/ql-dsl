using QL.Api.Operators;
using QL.Api.Types;

namespace QL.Core.Operators
{
    abstract class AbsoluteComparison : Operator
    {
        public override bool AcceptTypes(QLType leftHand, QLType rightHand)
        {
            if ((leftHand == QLType.Integer || leftHand == QLType.Decimal)
                && (rightHand == QLType.Integer || rightHand == QLType.Decimal))
            {
                return true;
            }
            return (leftHand == rightHand);
        }

        public override QLType ResultingType(QLType leftType, QLType rightType) {
            return QLType.Boolean;
        }
    }
}
