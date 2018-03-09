using QL.Api.Types;

namespace QL.Core.Interpreting.Operators
{
    internal class Or : Logical
    {
        public override Value Evaluate(Value leftHand, Value rightHand)
        {
            return new Value(leftHand.ToBoolean() || rightHand.ToBoolean(), QLType.Boolean);
        }
    }
}
