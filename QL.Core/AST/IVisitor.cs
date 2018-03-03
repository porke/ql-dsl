namespace QL.Core.Ast
{
    public interface IVisitor<T>
    {
        T Visit(BlockNode node);
        T Visit(QuestionNode node);
        T Visit(FormNode node);
        T Visit(ExpressionNode node);
        T Visit(VariableNode node);
        T Visit(LiteralNode node);
        T Visit(ConditionalNode node);      
    }
}
