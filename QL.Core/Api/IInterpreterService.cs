using QL.Core.Ast;
using QL.Core.Interpreting;
using QL.Core.Symbols;

namespace QL.Core.Api
{
    public interface IInterpreterService
    {
        Node EvaluateQuestionnaire(Node ast, MemorySystem memory, SymbolTable symbols);
    }
}
