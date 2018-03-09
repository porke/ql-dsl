using QL.Api.Types;

namespace QL.Core.Operators
{
    internal class And : Logical
    {
        public override Value Evaluate(Value leftHand, Value rightHand)
        {
            return new Value(leftHand.ToBoolean() && rightHand.ToBoolean(), QLType.Boolean);
        }
    }
}
