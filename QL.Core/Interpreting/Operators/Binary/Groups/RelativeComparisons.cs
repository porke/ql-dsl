using QL.Api.Operators;
using QL.Api.Types;

namespace QL.Core.Interpreting.Operators
{
    internal abstract class RelativeComparison : IOperator
    {
        public bool AcceptTypes(QLType leftHand, QLType rightHand)
        {
            bool integersOrDecimals = ((leftHand == QLType.Integer || leftHand == QLType.Decimal)
                && (rightHand == QLType.Integer || rightHand == QLType.Decimal));
            bool date = (leftHand == QLType.Date && rightHand == QLType.Date);

            return integersOrDecimals || date;
        }        

        public QLType ResultingType(QLType leftType, QLType rightType)
        {
            return QLType.Boolean;
        }

        public abstract Value Evaluate(Value leftInput, Value rightInput = null);
    }
}
