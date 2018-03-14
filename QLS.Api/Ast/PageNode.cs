using Antlr4.Runtime;

namespace QLS.Api.Ast
{
    public sealed class PageNode : Node
    {
        public PageNode(IToken token, string label) : base(token)
        {
            Label = label;
        }

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public string Label { get; }
    }
}
