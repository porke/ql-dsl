using QL.Api.Entities;
using QL.Api.Operators;

namespace QL.Core.Interpreting.Operators
{
    internal class BooleanNegation : IOperator
    {
        public string AsString
        {
            get
            {
                return "!";
            }
        }

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
