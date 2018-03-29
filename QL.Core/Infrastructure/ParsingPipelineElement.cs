using System.Linq;
using QL.Api.Infrastructure;
using Antlr4.Runtime;
using QL.Core.Parsing;
using Infrastructure;
using static QL.Core.Interpreting.Value;

namespace QL.Core.Infrastructure
{
    internal class ParsingPipelineElement : IPipelineElement<ParsingTask>
    {
        private bool _canContinue = true;

        public bool CanContinue => _canContinue;

        private QLParser SetupParser(string text)
        {
            var inputStream = new AntlrInputStream(text);
            var speakLexer = new QLLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(speakLexer);
            return new QLParser(commonTokenStream);
        }

        public ParsingTask Process(ParsingTask input)
        {            
            if (string.IsNullOrEmpty(input.ParsingInput))
            {
                input.Errors.Add("Input string is empty");
                _canContinue = false;
                return input;
            }

            QLParser parser = SetupParser(input.ParsingInput);
            var errorListener = new ErrorListener();
            parser.AddErrorListener(errorListener);

            var visitor = new ParseTreeVisitor(new OperatorFactory(new ValueFactory()));
            input.Ast = visitor.Visit(parser.form());
            errorListener.Errors.ForEach(x => input.Errors.Add(x));
            _canContinue = !errorListener.Errors.Any();

            return input;
        }
    }
}
