using QL.Core.Api;
using QL.Core.Ast;
using QL.Core.Symbols;

namespace QL.Core.Interpreting
{
    internal class InterpreterService : IInterpreterService
    {
        public Node EvaluateQuestionnaire(Node ast, MemorySystem memory, SymbolTable symbols)
        {
            var visitor = new InterpreterVisitor();
            return visitor.EvaluateAst(ast, memory, symbols);
        }
    }
}
