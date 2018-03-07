namespace QLS.Api.Ast
{
    public interface IParsingService
    {
        Node ParseQLSSheet(string qlsSheetText);
    }
}
