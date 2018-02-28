namespace QL.Core.Ast
{
    public interface IVisitor
    {
        void Visit(EmptyNode node);
        void Visit(QuestionNode node);
        void Visit(FormNode node);
        void Visit(ExpressionNode node);
        void Visit(VariableNode node);
        void Visit(LiteralNode node);
        void Visit(ConditionalNode node);
    }
}
