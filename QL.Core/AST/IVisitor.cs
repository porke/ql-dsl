namespace QL.Core.Ast
{
    public interface IVisitor
    {
        void VisitEnter(BlockNode node);
        void VisitEnter(QuestionNode node);
        void VisitEnter(FormNode node);
        void VisitEnter(ExpressionNode node);
        void VisitEnter(VariableNode node);
        void VisitEnter(LiteralNode node);
        void VisitEnter(ConditionalNode node);
        void VisitExit(BlockNode node);
        void VisitExit(QuestionNode node);
        void VisitExit(FormNode node);
        void VisitExit(ExpressionNode node);
        void VisitExit(VariableNode node);
        void VisitExit(LiteralNode node);
        void VisitExit(ConditionalNode node);
    }
}
