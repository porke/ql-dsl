using Antlr4.Runtime;
using QL.Core.Ast.Visitors;

namespace QL.Core.Ast
{
    public class ExpressionNode : Node
    {
        public ExpressionNode(IToken token, string opperator) : base(token)
        {
            Opperator = opperator;
        }

        public string Opperator { get; }

        protected override void VisitNode(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
