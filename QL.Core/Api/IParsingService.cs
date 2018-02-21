namespace QL.Core.Api
{
    public interface IParsingService
    {        
        ParsedSymbols ParseQLInput(string input);
    }
}
