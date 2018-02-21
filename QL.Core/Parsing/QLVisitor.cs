using System.Linq;
using QL.Core.AST;
using System.Collections.Generic;
using static QL.Core.QLParser;

namespace QL.Core.Parsing
{
    internal class QLVisitor : QLBaseVisitor<string>
    {
        public IList<QLForm> AST { get; } = new List<QLForm>();
        public Stack<List<QLStatement>> StatementListStack = new Stack<List<QLStatement>>();
        public Stack<QLExpression> ExpresionStack = new Stack<QLExpression>();

        public override string VisitForm(FormContext context)
        {
            QLForm form = new QLForm { Label = context.LABEL().GetText() };

            BlockContext block = context.block();
            if (block != null)
            {
                StatementListStack.Push(form.Statements);
                Visit(block);
                StatementListStack.Pop();
            }

            AST.Add(form);
            return string.Empty;
        }

        public override string VisitBlock(BlockContext context)
        {
            List<StatementContext> statements = context.statement().ToList();
            statements.ForEach(x => Visit(x));

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
            var question = new QLQuestion
            {
                Label = context.name().LABEL().GetText(),
                Description = context.description().STR().GetText().Replace("\"", string.Empty),
                Type = context.type().GetText()
            };
            
            ExpressionContext expression = context.expression();
            if (expression != null)
            {
                ExpresionStack.Push(question.Expression);
                Visit(expression);
                ExpresionStack.Pop();
            }

            StatementListStack.Peek().Add(question);

            return string.Empty;
        }

        public override string VisitConditional(ConditionalContext context)
        {
            QLConditional conditional = new QLConditional();

            ExpressionContext expression = context.expression();
            if (expression != null)
            {
                ExpresionStack.Push(conditional.Expression);
                Visit(expression);
                ExpresionStack.Pop();
            }

            BlockContext block = context.block(0);
            if (block != null)
            {
                StatementListStack.Push(conditional.IfStatements);
                Visit(block);
                ExpresionStack.Pop();
            }

            block = context.block(1);
            if (block != null)
            {
                StatementListStack.Push(conditional.ElseStatements);
                Visit(block);
                ExpresionStack.Pop();
            }

            StatementListStack.Peek().Add(conditional);
            return string.Empty;
        }

        public override string VisitExpression(ExpressionContext context)
        {
            UnOpContext unOp = context.unOp();
            BinOpContext binOp = context.binOp();
            LiteralContext literal = context.literal();
            ExpressionContext subexpression = context.expression(0);
            var label = context.LABEL();

            if (unOp != null)
            {
                VisitUnExpresion(context);
            }
            else if (binOp != null)
            {
                VisitBinExpresion(context);
            }
            else if (literal != null)
            {
                VisitLitExpresion(context);
            }
            else if (subexpression != null)
            {
                Visit(subexpression);
            }
            else if (label != null)
            {
                VisitVarExpresion(context);
            }

            return string.Empty;
        }

        private void VisitUnExpresion(ExpressionContext context) {

        }

        private void VisitBinExpresion(ExpressionContext context)
        {

        }

        private void VisitLitExpresion(ExpressionContext context)
        {

        }

        private void VisitVarExpresion(ExpressionContext context)
        {

        }

    }
}
