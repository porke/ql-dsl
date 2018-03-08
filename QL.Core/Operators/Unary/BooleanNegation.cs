using QL.Core.Types;

namespace QL.Core.Operators
{
    internal class BooleanNegation : Operator
    {
        public override Value Evaluate(Value input, Value empty = null)
        {
            return new Value(!input.ToBoolean(), QLType.Boolean);
        }
    }
}
