namespace QL.Core.Ast
{
    public abstract class BaseVisitor : IVisitor
    {
        public virtual void Visit(FormNode node)
        {
        }

        public virtual void Visit(VariableNode node)
        {
        }

        public virtual void Visit(ConditionalNode node)
        {
        }

        public virtual void Visit(LiteralNode node)
        {
        }

        public virtual void Visit(ExpressionNode node)
        {
        }

        public virtual void Visit(QuestionNode node)
        {
        }

        public virtual void Visit(EmptyNode node)
        {
        }
    }
}
