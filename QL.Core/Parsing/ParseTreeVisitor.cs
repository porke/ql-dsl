using QL.Core.AST;
using System.Collections.Generic;
using static QL.Core.QLParser;
using Antlr4.Runtime.Tree;

namespace QL.Core.Parsing
{
    internal class ParseTreeVisitor : QLBaseVisitor<Node>
    {
        public override Node Visit(IParseTree tree)
        {
            if (tree == null)
            {
                return new TerminalNode();
            }

            return base.Visit(tree);
        }

        public override Node VisitForm(FormContext context)
        {
            return new Form
            {
                Label = context.LABEL().GetText(),
                Block = Visit(context.block()) as Block
            };
        }

        public override Node VisitBlock(BlockContext context)
        {
            var statementNodeList = new List<Statement>();
            foreach (StatementContext x in context.statement())
            {
                statementNodeList.Add(Visit(x) as Statement);
            }

            return new Block(new Statements(statementNodeList));
        }

        public override Node VisitStatement(StatementContext context)
        {
            QuestionContext question = context.question();
            ConditionalContext conditional = context.conditional();
            if (question != null)
            {
                return Visit(question);
            }
            else
            {
                return Visit(conditional);
            }
        }

        public override Node VisitQuestion(QuestionContext context)
        {
            return new Question
            {
                Variable = new Variable { Label = context.name().LABEL().GetText() },
                Description = context.description().STR().GetText().Replace("\"", string.Empty),
                Type = context.type().GetText(),
                Expression = Visit(context.expression()) as Expression
            };
        }

        public override Node VisitConditional(ConditionalContext context)
        {            
            return new Conditional(Visit(context.expression()) as Expression,
                                   Visit(context.ifBlock()) as Block,
                                   Visit(context.elseBlock()) as Block);
        }

        public override Node VisitIfBlock(IfBlockContext context)
        {
            return new TerminalNode();
        }

        public override Node VisitElseBlock(ElseBlockContext context)
        {
            return new TerminalNode();
        }

        public override Node VisitExpression(ExpressionContext context)
        {
            return new TerminalNode();
        }

        public override Node VisitLiteralExpression(LiteralExpressionContext context)
        {
            return new TerminalNode();
        }
    }
}
