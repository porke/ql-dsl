using QL.Core.Types;

namespace QL.Core.Operators
{
    abstract class Logical : Operator
    {
        public override bool AcceptTypes(QLType leftHand, QLType rightHand)
        {
            return (leftHand == QLType.Boolean && rightHand == QLType.Boolean);
        }

        public override QLType ResultingType(QLType leftType, QLType rightType)
        {
            return QLType.Boolean;
        }
    }
}
