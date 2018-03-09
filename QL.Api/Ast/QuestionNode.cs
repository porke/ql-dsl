using Antlr4.Runtime;
using QL.Api.Types;

namespace QL.Api.Ast
{
    public class QuestionNode : Node
    {
        public QuestionNode(IToken token, string description, string label, bool isEvaluated, QLType type) : base(token)
        {
            Description = description;
            Label = label;
            Type = type;
            IsEvaluated = isEvaluated;
        }

        public string Description { get; }
        public QLType Type { get; }
        public string Label { get; }
        public bool IsEvaluated { get; }

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
