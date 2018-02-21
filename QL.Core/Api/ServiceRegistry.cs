using QL.Core.Parsing;

namespace QL.Core.Api
{
    public static class ServiceRegistry
    {
        private static IQLParsingService _parsingService = new QLParsingService();

        public static IQLParsingService ParsingService => _parsingService;
    }
}
