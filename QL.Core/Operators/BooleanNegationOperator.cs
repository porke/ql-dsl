using QL.Core.Types;

namespace QL.Core.Operators
{
    internal class BooleanNegationOperator : IUnaryOperator
    {
        public Value Evaluate(Value input)
        {
            return new Value(!input.ToBool(), QLType.Boolean);
        }
    }
}
