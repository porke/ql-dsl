using Antlr4.Runtime;

namespace QL.Api.Ast
{
    public sealed class BlockNode : Node
    {
        public BlockNode(IToken token, int depth) : base(token)
        {
            Depth = depth;
        }

        public int Depth { get; private set; }

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
