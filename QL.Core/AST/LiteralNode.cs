using System;
using Antlr4.Runtime;
using QL.Core.Ast.Visitors;

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
            // TODO: implement soon
        }
    }
}
