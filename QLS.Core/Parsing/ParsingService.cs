using System;
using QLS.Api.Ast;
using Antlr4.Runtime;

namespace QLS.Core.Parsing
{
    internal class ParsingService : IParsingService
    {
        private QLSParser SetupParser(string text)
        {
            var inputStream = new AntlrInputStream(text);
            var speakLexer = new QLSLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(speakLexer);
            return new QLSParser(commonTokenStream);
        }

        public Node ParseQLSSheet(string qlsSheetText)
        {
            QLSParser parser = SetupParser(qlsSheetText);
            var visitor = new ParseTreeVisitor();
            return visitor.Visit(parser.stylesheet());
        }
    }
}
