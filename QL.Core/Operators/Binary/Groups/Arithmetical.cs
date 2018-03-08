using QL.Core.Types;

namespace QL.Core.Operators
{
    abstract class Arithmetical : Operator
    {
        public override bool AcceptTypes(QLType leftHand, QLType rightHand)
        {
            return ((leftHand == QLType.Integer || leftHand == QLType.Decimal)
                && (rightHand == QLType.Integer || rightHand == QLType.Decimal));
        }

        public override QLType ResultingType(QLType leftType, QLType rightType)
        {
            if (leftType == rightType)
            {
                return leftType;
            }
            return QLType.Decimal;
        }
    }
}
