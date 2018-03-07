using Antlr4.Runtime;

namespace QLS.Api.Ast
{
    public class StylesheetNode : Node
    {
        public StylesheetNode(IToken token, string label) : base(token)
        {
            Label = label;
        }

        public string Label { get; }

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
