using Antlr4.Runtime;

namespace QL.Core.Ast
{
    public class VariableNode : Node
    {
        public VariableNode(IToken token, string label) : base(token)
        {
            Label = label;
        }

        public string Label { get; }

        protected override void VisitNode(IVisitor visitor)
        {
            visitor.VisitEnter(this);
            VisitChildren(visitor);
            visitor.VisitExit(this);
        }
    }
}
