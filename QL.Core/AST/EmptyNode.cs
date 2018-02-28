using Antlr4.Runtime;

namespace QL.Core.Ast
{
    public class EmptyNode : Node
    {
        public EmptyNode(IToken token) : base(token)
        {
            
        }

        protected override void VisitNode(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
