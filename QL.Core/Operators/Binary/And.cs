using QL.Core.Types;

namespace QL.Core.Operators
{
    internal class And : Logical
    {
        public override Value Evaluate(Value lhs, Value rhs)
        {
            return new Value(lhs.ToBoolean() && rhs.ToBoolean(), QLType.Boolean);
        }
    }
}
