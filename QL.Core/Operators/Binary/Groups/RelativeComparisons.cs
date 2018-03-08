using QL.Core.Types;

namespace QL.Core.Operators
{
    abstract class RelativeComparison : Operator
    {
        public override bool AcceptTypes(QLType leftHand, QLType rightHand)
        {
            bool integersOrDecimals = ((leftHand == QLType.Integer || leftHand == QLType.Decimal)
                && (rightHand == QLType.Integer || rightHand == QLType.Decimal));
            bool date = (leftHand == QLType.Date && rightHand == QLType.Date);

            return integersOrDecimals || date;
        }

        public override QLType ResultingType(QLType leftType, QLType rightType)
        {
            return QLType.Boolean;
        }
    }
}
