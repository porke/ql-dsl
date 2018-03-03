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

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            var returnValue = visitor.Visit(this);
            VisitChildren(visitor);
            return returnValue;
        }
    }
}
