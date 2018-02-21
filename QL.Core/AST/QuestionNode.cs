using Antlr4.Runtime;
using QL.Core.Ast.Visitors;

namespace QL.Core.Ast
{
    public class QuestionNode : Node
    {
        public QuestionNode(IToken token, string description, string label, string type) : base(token)
        {
            Description = description;
            Label = label;
            Type = type;
        }

        public string Description { get; }        
        public string Type { get; }
        public string Label { get; }

        protected override void VisitNode(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
