using QL.Core.Api;
using Antlr4.Runtime;

namespace QL.Core.Parsing
{
    internal class ParsingService : IParsingService
    {        
        private QLParser SetupParser(string text)
        {
            var inputStream = new AntlrInputStream(text);
            var speakLexer = new QLLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(speakLexer);
            return new QLParser(commonTokenStream);
        }

        public ParsedSymbols ParseQLInput(string input)
        {
            var parser = SetupParser(input);
            var errorListener = new ErrorListener();
            parser.AddErrorListener(errorListener);
            var visitor = new ParseTreeVisitor();
            return new ParsedSymbols(visitor.Visit(parser.form()), errorListener.Errors);
        }
    }
}
