using QL.Core.Api;
using QL.Core.Ast;

namespace QL.Core.Interpreting
{
    internal class InterpreterService : IInterpreterService
    {
        public Node EvaluateQuestionnaire(Node ast, MemorySystem memory)
        {
            var visitor = new InterpreterVisitor();
            return visitor.EvaluateAst(ast, memory);
        }
    }
}
