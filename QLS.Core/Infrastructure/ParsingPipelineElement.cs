using Antlr4.Runtime;
using Infrastructure;
using QLS.Api.Infrastructure;
using QLS.Core.Parsing;

namespace QLS.Core.Infrastructure
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
