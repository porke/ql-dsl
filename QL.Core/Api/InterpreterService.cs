using QL.Core.Ast;

namespace QL.Core.Api
{
    public interface IInterpreterService
    {
        FormNode AssignValue<T>(string questionId, T value);
    }
}
