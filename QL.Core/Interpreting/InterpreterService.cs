using QL.Core.Api;
using QL.Core.Ast;
using System;

namespace QL.Core.Interpreting
{
    internal class InterpreterService : IInterpreterService
    {
        public Node AssignValue<T>(string questionId, T value, Node ast)
        {
            throw new NotImplementedException();
        }

        public Node EvaluateAst(Node ast)
        {
            var visitor = new InterpreterVisitor();
            return visitor.EvaluateAst(ast);
        }
    }
}
