using Antlr4.Runtime;
using QL.Core.Operators;

namespace QL.Core.Ast
{
    public class ExpressionNode : Node
    {
        public ExpressionNode(IToken token, Operator @operator) : base(token)
        {
            Operator = @operator;
        }

        public Operator Operator { get; }
        public bool IsBinary => ChildNodes.Count == 2;
        public bool IsUnary => ChildNodes.Count == 1;

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
