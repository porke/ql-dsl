using QL.Core.Api;
using Antlr4.Runtime;
using static QL.Core.QLParser;

namespace QL.Core.Parsing
{
    internal class QLParsingService : IQLParsingService
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

            FormContext context = parser.form();
            var visitor = new QLVisitor();
            visitor.Visit(context);

            return new ParsedSymbols
            {
                Forms = visitor.AST
            };
        }
    }
}
