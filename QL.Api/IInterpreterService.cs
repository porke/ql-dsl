using QL.Api.Ast;
using QL.Api.Entities;

namespace QL.Api
{
    public interface IInterpreterService
    {
        Node EvaluateQuestionnaire(Node ast, MemorySystem memory, SymbolTable symbols);
    }
}
