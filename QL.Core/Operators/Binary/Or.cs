using QL.Core.Types;

namespace QL.Core.Operators
{
    internal class Or : Logical
    {
        public override Value Evaluate(Value leftHand, Value rightHand)
        {
            return new Value(leftHand.ToBoolean() || rightHand.ToBoolean(), QLType.Boolean);
        }
    }
}
