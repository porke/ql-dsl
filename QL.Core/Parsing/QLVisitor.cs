using System.Linq;
using QL.Core.AST;
using System.Collections.Generic;
using static QL.Core.QLParser;

namespace QL.Core.Parsing
{
    internal class QLVisitor : QLBaseVisitor<string>
    {
        public IList<QLForm> Forms { get; } = new List<QLForm>();
        public IList<QLQuestion> Questions { get; set; } = new List<QLQuestion>();

        public override string VisitForm(FormContext context)
        {
            Forms.Add(new QLForm { Label = context.LABEL().GetText() });

            StatementsContext statements = context.statements();
            if (statements != null)
            {
                Visit(statements);
            }            

            return string.Empty;
        }

        public override string VisitStatements(StatementsContext context)
        {
            Visit(context.question());

            List<StatementsContext> statements = context.statements().ToList();
            statements.ForEach(x => Visit(x));
           
            return string.Empty;
        }

        public override string VisitQuestion(QuestionContext context)
        {
            var question = new QLQuestion
            {
                Label = context.name().LABEL().GetText(),
                Description = context.description().STR().GetText().Replace("\"", string.Empty),
                Type = context.type().GetText()
            };
            Questions.Add(question);

            ExpressionContext expression = context.expression();
            if (expression != null)
            {
                Visit(expression);
            }            

            return string.Empty;
        }

        public override string VisitExpression(ExpressionContext context)
        {
            if (context.LABEL() != null)
            {
                if (context.Parent is QuestionContext)
                {
                    string parentQuestionName = (context.Parent as QuestionContext).name().LABEL().GetText();
                    var parentQuestion = Questions.FirstOrDefault(x => x.Label == parentQuestionName);
                    parentQuestion.Expression = new QLExpression { Label = context.LABEL().GetText() };
                }
            }
            else if (context.expression().Length > 0)
            {
                context.expression().ToList().ForEach(x => Visit(x));
            }

            return string.Empty;
        }
    }
}
