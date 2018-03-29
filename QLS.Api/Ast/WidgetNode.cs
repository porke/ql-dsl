using Antlr4.Runtime;
using QLS.Api.Entities;

namespace QLS.Api.Ast
{
    public sealed class WidgetNode : Node
    {
        public WidgetNode(IToken token, IWidget widget) : base(token)
        {
            Widget = widget;
        }

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public IWidget Widget { get; }
    }
}
