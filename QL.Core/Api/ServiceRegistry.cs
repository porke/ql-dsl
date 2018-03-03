using QL.Core.Interpreting;
using QL.Core.Parsing;

namespace QL.Core.Api
{
    public static class ServiceRegistry
    {
        private static IParsingService _parsingService = new ParsingService();
        private static IInterpretingService _interpretingService = new InterpretingService();

        public static IParsingService ParsingService => _parsingService;
        public static IInterpretingService InterpretingService => _interpretingService;
    }
}
