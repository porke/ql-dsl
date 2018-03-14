using Antlr4.Runtime;

namespace QL.Api.Ast
{
    public sealed class ConditionalNode : Node
    {
        public ConditionalNode(IToken token) : base(token)
        {

        }

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
