namespace QL.Core.Api
{
    public interface IQLParsingService
    {        
        ParsedSymbols ParseQLInput(string input);
    }
}
