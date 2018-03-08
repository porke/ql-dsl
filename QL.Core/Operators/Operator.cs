using QL.Core.Types;

namespace QL.Core.Operators
{
    public abstract class Operator
    {
        // TODO: find a better way to implement 'optinal' arguments.
        // Need Operator type in AST, but unary and binary functions need different number of arguments for these functions.
        abstract public Value Evaluate(Value leftInput, Value rightInput = null);
        abstract public bool AcceptTypes(QLType leftInput, QLType rightInput = QLType.Undefined);
        abstract public QLType ResultingType(QLType leftInput, QLType rightInput = QLType.Undefined);
    }
}
