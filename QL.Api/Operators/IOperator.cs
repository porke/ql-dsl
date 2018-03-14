using QL.Api.Entities;

namespace QL.Api.Operators
{
    public interface IOperator
    {
        IValue Evaluate(IValue leftInput, IValue rightInput = null);
        bool AcceptTypes(QLType leftInput, QLType rightInput = QLType.Undefined);
        QLType ResultingType(QLType leftInput, QLType rightInput = QLType.Undefined);
    }
}
