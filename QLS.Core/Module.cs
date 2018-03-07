using QLS.Api.Ast;
using QLS.Core.Parsing;

namespace QLS.Core
{
    public static class Module
    {
        public static IParsingService ParsingService { get; }

        static Module()
        {
            ParsingService = new ParsingService();
        }
    }
}
