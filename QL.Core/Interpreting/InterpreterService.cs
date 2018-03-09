using QL.Api;
using QL.Api.Ast;
using QL.Api.Entities;

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
