using System.Linq;
using System.Collections.Generic;
using QL.Core.Api;
using QL.Core.AST;
using Antlr4.Runtime;
using static QL.Core.QLParser;

namespace QL.Core.Parsing
{
    public class QLParsingService : IQLParsingService
    {
        public IList<QLQuestion> Questions { get; private set; } = new List<QLQuestion>();

        private QLParser SetupParser(string text)
        {
            var inputStream = new AntlrInputStream(text);
            var speakLexer = new QLLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(speakLexer);

            return new QLParser(commonTokenStream);
        }

        public void ParseQLInput(string input)
        {
            var parser = SetupParser(input);

            QuestionContext context = parser.question();
            var visitor = new QLQuestionVisitor();

            visitor.Visit(context);

            visitor.Questions.ToList().ForEach(x => Questions.Add(x));
        }
    }
}
