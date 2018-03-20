using Antlr4.Runtime;

namespace QLS.Api.Ast
{
    public sealed class StyleNode : Node
    {
        public StyleNode(IToken token, string targetType) : base(token)
        {
            TargetType = targetType;
        }

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public string TargetType { get; }
    }
}
