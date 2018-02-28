using Antlr4.Runtime;

namespace QL.Core.Ast
{
    public class LiteralNode : Node
    {
        public LiteralNode(IToken token, string value) : base(token)
        {
            Value = value;
        }

        public string Value { get; }

        protected override void VisitNode(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
