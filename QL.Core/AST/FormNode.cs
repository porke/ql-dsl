using Antlr4.Runtime;

namespace QL.Core.Ast
{
    public class FormNode : Node
    {
        public FormNode(IToken token, string label) : base(token)
        {
            Label = label;
        }

        public string Label { get; }

        protected override void VisitNode(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
