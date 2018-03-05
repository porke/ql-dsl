using QL.Core.Ast;
using QL.Core.Interpreting;

namespace QL.Core.Api
{
    public interface IInterpreterService
    {
        Node EvaluateQuestionnaire(Node ast, MemorySystem memory);
    }
}
