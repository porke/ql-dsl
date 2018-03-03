namespace QL.Core.Ast
{
    public sealed class NullNode : Node
    {
        public NullNode() : base(null)
        {
        }

        protected override T VisitNode<T>(IVisitor<T> visitor)
        {
            // Visiting a null node yields no result
            return default(T);
        }
    }
}
