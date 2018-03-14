using Antlr4.Runtime;
using QLS.Api.Entities;

namespace QLS.Api.Ast
{
    public sealed class WidgetNode : Node
    {
        public WidgetNode(IToken token, WidgetType widgetType) : base(token)
        {
            WidgetType = widgetType;
        }

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public WidgetType WidgetType { get; private set; }
    }
}
