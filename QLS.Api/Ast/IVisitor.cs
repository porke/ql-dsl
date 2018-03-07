namespace QLS.Api.Ast
{
    public interface IVisitor<T>
    {
        T Visit(PageNode node);
        T Visit(StylesheetNode node);
    }
}
