﻿using Antlr4.Runtime;

namespace QL.Api.Ast
{
    public sealed class VariableNode : Node
    {
        public VariableNode(IToken token, string label) : base(token)
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
