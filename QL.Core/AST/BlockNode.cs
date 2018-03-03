using Antlr4.Runtime;

namespace QL.Core.Ast
{
    public class BlockNode : Node
    {
        public BlockNode(IToken token, int depth) : base(token)
        {
            Depth = depth;
        }

        public int Depth { get; private set; }

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            var returnValue = visitor.Visit(this);
            VisitChildren(visitor);
            return returnValue;
        }
    }
}
