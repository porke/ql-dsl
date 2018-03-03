namespace QL.Core.Ast
{
    public abstract class BaseVisitor<T> : IVisitor<T>
    {
        public virtual T Visit(FormNode node) { return default(T); }
        public virtual T Visit(VariableNode node) { return default(T); }
        public virtual T Visit(ConditionalNode node) { return default(T); }
        public virtual T Visit(LiteralNode node) { return default(T); }
        public virtual T Visit(ExpressionNode node) { return default(T); }
        public virtual T Visit(QuestionNode node) { return default(T); }
        public virtual T Visit(BlockNode node) { return default(T); }        
    }
}
