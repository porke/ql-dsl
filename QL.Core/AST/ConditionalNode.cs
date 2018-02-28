using Antlr4.Runtime;

namespace QL.Core.Ast
{
    public class ConditionalNode : Node
    {
        public ConditionalNode(IToken token) : base(token)
        {

        }

        protected override void VisitNode(IVisitor visitor)
        {
            visitor.VisitEnter(this);
            VisitChildren(visitor);
            visitor.VisitExit(this);
        }
    }
}
