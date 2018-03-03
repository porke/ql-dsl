using QL.Core.Ast;

namespace QL.Core.Api
{
    public interface IInterpretingService
    {
        FormNode AssignValue<T>(string questionId, T value);
    }
}
