using QL.Api.Types;

namespace QL.Api.Operators
{
    public interface IOperator
    {
        // TODO: find a better way to implement 'optinal' arguments.
        // Need Operator type in AST, but unary and binary functions need different number of arguments for these functions.
        Value Evaluate(Value leftInput, Value rightInput = null);
        bool AcceptTypes(QLType leftInput, QLType rightInput = QLType.Undefined);
        QLType ResultingType(QLType leftInput, QLType rightInput = QLType.Undefined);
    }
}
