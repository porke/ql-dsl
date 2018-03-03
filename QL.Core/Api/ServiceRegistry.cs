using QL.Core.Interpreting;
using QL.Core.Parsing;

namespace QL.Core.Api
{
    public static class ServiceRegistry
    {
        private static IParserService _parsingService = new ParserService();
        private static IInterpreterService _interpretingService = new InterpreterService();

        public static IParserService ParsingService => _parsingService;
        public static IInterpreterService InterpretingService => _interpretingService;
    }
}
