using Antlr4.Runtime;
using QL.Core.Types;

namespace QL.Core.Ast
{
    public class QuestionNode : Node
    {
        public QuestionNode(IToken token, string description, string label, QLType type) : base(token)
        {
            Description = description;
            Label = label;
            Type = type;
        }

        public string Description { get; }
        public QLType Type { get; }
        public string Label { get; }

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            var returnValue = visitor.Visit(this);
            VisitChildren(visitor);
            return returnValue;
        }
    }
}
