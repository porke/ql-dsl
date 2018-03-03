using Antlr4.Runtime;

namespace QL.Core.Ast
{
    public class ConditionalNode : Node
    {
        public ConditionalNode(IToken token) : base(token)
        {

        }

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            var returnValue = visitor.Visit(this);
            VisitChildren(visitor);
            return returnValue;
        }
    }
}
