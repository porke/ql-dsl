namespace QL.Core.Api
{
    public interface IParserService
    {        
        ParsedSymbols ParseQLInput(string input);
    }
}
