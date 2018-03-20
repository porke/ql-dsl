namespace QLS.Api.Ast
{
    public interface IVisitor<T>
    {
        T Visit(PageNode node);
        T Visit(StylesheetNode node);
        T Visit(SectionNode node);
        T Visit(QuestionNode node);
        T Visit(WidgetNode node);
        T Visit(PropertyNode node);
        T Visit(StyleNode node);
    }
}
