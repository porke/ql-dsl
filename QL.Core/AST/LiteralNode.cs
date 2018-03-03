using Antlr4.Runtime;
using QL.Core.Types;

namespace QL.Core.Ast
{
    public class LiteralNode : Node
    {
        public LiteralNode(IToken token, string value, QLType type) : base(token)
        {
            Value = value;
            Type = type;
        }

        public string Value { get; }
        public QLType Type { get; }

        protected override void VisitNode(IVisitor visitor)
        {
            visitor.VisitEnter(this);
            VisitChildren(visitor);
            visitor.VisitExit(this);
        }
    }
}
