using Antlr4.Runtime.Misc;

namespace QL.Core.Parsing
{
    public class QLQuestionVisitor : QLBaseVisitor<string>
    {
        public override string VisitQuestion([NotNull] QLParser.QuestionContext context)
        {
            return base.VisitQuestion(context);
        }
    }
}
