using Antlr4.Runtime.Misc;
using QL.Core.AST;
using System.Collections.Generic;

namespace QL.Core.Parsing
{
    public class QLQuestionVisitor : QLBaseVisitor<string>
    {
        public IList<QLQuestion> Questions { get; set; } = new List<QLQuestion>();

        public override string VisitQuestion([NotNull] QLParser.QuestionContext context)
        {
            var question = new QLQuestion
            {
                Label = context.name().LABEL().GetText(),
                Description = context.description().STR().GetText().Replace("\"", string.Empty),
                Type = context.type().GetText()
            };

            Questions.Add(question);

            return string.Empty;
        }        
    }
}
