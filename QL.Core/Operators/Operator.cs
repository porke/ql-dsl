using QL.Core.Types;

namespace QL.Core.Operators
{
    public abstract class Operator
    {
        abstract public Value Evaluate(Value leftInput, Value rightInput = null);
    }
}
