using QL.Api;
using QL.Core.Interpreting;
using QL.Core.Parsing;

namespace QL.Core
{
    public class Module
    {         
        public static IParserService ParsingService { get; }
        public static IInterpreterService InterpretingService { get; }

        static Module()
        {
            ParsingService = new ParserService();
            InterpretingService = new InterpreterService();
        }
    }
}
