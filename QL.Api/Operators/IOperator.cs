using QL.Api.Entities;

namespace QL.Api.Operators
{
    public interface IOperator
    {
        Value Evaluate(Value leftInput, Value rightInput = null);
        bool AcceptTypes(QLType leftInput, QLType rightInput = QLType.Undefined);
        QLType ResultingType(QLType leftInput, QLType rightInput = QLType.Undefined);
        string AsString { get; }
    }
}
