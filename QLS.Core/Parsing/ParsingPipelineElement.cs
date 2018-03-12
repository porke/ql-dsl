using Antlr4.Runtime;
using QLS.Api.Infrastructure;

namespace QLS.Core.Parsing
{
    internal class ParsingPipelineElement : IPipelineElement<StylesheetTask>
    {
        public bool CanContinue => true;

        private QLSParser SetupParser(string text)
        {
            var inputStream = new AntlrInputStream(text);
            var speakLexer = new QLSLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(speakLexer);
            return new QLSParser(commonTokenStream);
        }
        
        public StylesheetTask Process(StylesheetTask input)
        {
            QLSParser parser = SetupParser(input.StylesheetText);
            var visitor = new ParseTreeVisitor();
            input.Ast = visitor.Visit(parser.stylesheet());
            return input;
        }
    }
}
