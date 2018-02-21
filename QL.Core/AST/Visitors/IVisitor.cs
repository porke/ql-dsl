namespace QL.Core.Ast.Visitors
{
    public interface IVisitor
    {
        void Visit(EmptyNode node);
        void Visit(QuestionNode node);
        void Visit(FormNode node);
    }
}
