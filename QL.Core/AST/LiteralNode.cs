using Antlr4.Runtime;

namespace QL.Core.AST
{
    public class LiteralNode : Node
    {
        public LiteralNode(IToken token, string value) : base(token)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
