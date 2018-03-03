using Antlr4.Runtime;

namespace QL.Core.Ast
{
    public class ExpressionNode : Node
    {
        public ExpressionNode(IToken token, string @operator) : base(token)
        {
            Operator = @operator;
        }

        public string Operator { get; }

        protected override void VisitNode(IVisitor visitor)
        {
            visitor.VisitEnter(this);
            VisitChildren(visitor);
            visitor.VisitExit(this);
        }
    }
}
