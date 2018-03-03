using QL.Core.Ast;

namespace QL.Core.Api
{
    public interface IInterpreterService
    {
        Node AssignValue<T>(string questionId, T value, Node ast);
        Node EvaluateAst(Node ast);
    }
}
