using QL.Core.Types;

namespace QL.Core.Operators
{
    internal interface IUnaryOperator
    {
        Value Evaluate(Value input);
    }
}
