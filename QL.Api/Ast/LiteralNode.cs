using Antlr4.Runtime;
using QL.Api.Types;

namespace QL.Api.Ast
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

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
