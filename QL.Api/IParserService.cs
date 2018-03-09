namespace QL.Api
{
    public interface IParserService
    {        
        ParsedSymbols ParseQLInput(string input);
    }
}
