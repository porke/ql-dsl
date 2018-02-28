namespace QL.Core.Ast
{
    public abstract class BaseVisitor : IVisitor
    {
        public virtual void VisitEnter(FormNode node) { }
        public virtual void VisitEnter(VariableNode node) { }
        public virtual void VisitEnter(ConditionalNode node) { }
        public virtual void VisitEnter(LiteralNode node) { }
        public virtual void VisitEnter(ExpressionNode node) { }
        public virtual void VisitEnter(QuestionNode node) { }
        public virtual void VisitEnter(BlockNode node) { }
        public virtual void VisitExit(FormNode node) { }
        public virtual void VisitExit(VariableNode node) { }
        public virtual void VisitExit(ConditionalNode node) { }
        public virtual void VisitExit(LiteralNode node) { }
        public virtual void VisitExit(ExpressionNode node) { }
        public virtual void VisitExit(QuestionNode node) { }
        public virtual void VisitExit(BlockNode node) { }
    }
}
