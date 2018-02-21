using System.Linq;
using QL.Core.AST;
using System.Collections.Generic;
using static QL.Core.QLParser;

namespace QL.Core.Parsing
{
    internal class QLVisitor : QLBaseVisitor<string>
    {
        public IList<Form> AST { get; } = new List<Form>();
        public Stack<Node> ParentStack = new Stack<Node>();

        public override string VisitForm(FormContext context)
        {
            Form form = new Form { Label = context.LABEL().GetText() };

            BlockContext block = context.block();
            if (block != null)
            {
                ParentStack.Push(form);
                Visit(block);
                ParentStack.Pop();
            }

            AST.Add(form);
            return string.Empty;
        }

        public override string VisitBlock(BlockContext context)
        {
            Statements statements = new Statements();
            List<StatementContext> statementList = context.statement().ToList();

            foreach (StatementContext x in statementList)
            {
                ParentStack.Push(statements);
                Visit(x);
                ParentStack.Pop();
            }

            (ParentStack.Peek() as Form).Statements = statements;
            return string.Empty;
        }

        public override string VisitStatement(StatementContext context)
        {
            QuestionContext question = context.question();
            ConditionalContext conditional = context.conditional();
            if (question != null)
            {
                Visit(question);
            } else {
                Visit(conditional);
            }
            return string.Empty;
        }

        public override string VisitQuestion(QuestionContext context)
        {
            var question = new Question
            {
                Variable = new Variable { Label = context.name().LABEL().GetText() },
                Description = context.description().STR().GetText().Replace("\"", string.Empty),
                Type = context.type().GetText()
            };
            
            ExpressionContext expression = context.expression();
            if (expression != null)
            {
                ParentStack.Push(question.Expression);
                Visit(expression);
                ParentStack.Pop();
            }

            (ParentStack.Peek() as Statements).StatementList.Add(question);

            return string.Empty;
        }

        public override string VisitConditional(ConditionalContext context)
        {
            Conditional conditional = new Conditional();

            ExpressionContext expression = context.expression();
            if (expression != null)
            {
                ParentStack.Push(conditional.Expression);
                Visit(expression);
                ParentStack.Pop();
            }

            BlockContext block = context.block(0);
            if (block != null)
            {
                ParentStack.Push(conditional);
                Visit(block);
                ParentStack.Pop();
            }

            block = context.block(1);
            if (block != null)
            {
                ParentStack.Push(conditional);
                Visit(block);
                ParentStack.Pop();
            }

            // TODO: Fix for Statements, so far only works for forms
            (ParentStack.Peek() as Statements).StatementList.Add(conditional);
            return string.Empty;
        }

        public override string VisitExpression(ExpressionContext context)
        {
       
            return string.Empty;
        }

    }
}
