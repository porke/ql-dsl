using QL.Core.Types;

namespace QL.Core.Operators
{
    internal interface IBinaryOperator
    {
        Value Evaluate(Value lhs, Value rhs);
    }
}
