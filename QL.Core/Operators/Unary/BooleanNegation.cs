using QL.Core.Types;

namespace QL.Core.Operators
{
    internal class BooleanNegation : Operator
    {
        public override Value Evaluate(Value input, Value empty = null)
        {
            return new Value(!input.ToBoolean(), QLType.Boolean);
        }

        public override bool AcceptTypes(QLType value, QLType empty = QLType.Undefined)
        {
            return (value == QLType.Boolean);
        }

        public override QLType ResultingType(QLType value, QLType empty = QLType.Undefined)
        {
            return QLType.Boolean;
        }
    }
}
