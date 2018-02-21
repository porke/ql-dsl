using QL.Core.Parsing;

namespace QL.Core.Api
{
    public static class ServiceRegistry
    {
        private static IParsingService _parsingService = new ParsingService();

        public static IParsingService ParsingService => _parsingService;
    }
}
