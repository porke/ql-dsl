using QL.Api.Operators;
using QL.Api.Types;

namespace QL.Core.Interpreting.Operators
{
    internal class BooleanNegation : IOperator
    {
        public Value Evaluate(Value input, Value empty = null)
        {
            return new Value(!input.ToBoolean(), QLType.Boolean);
        }

        public bool AcceptTypes(QLType value, QLType empty = QLType.Undefined)
        {
            return (value == QLType.Boolean);
        }

        public QLType ResultingType(QLType value, QLType empty = QLType.Undefined)
        {
            return QLType.Boolean;
        }
    }
}
