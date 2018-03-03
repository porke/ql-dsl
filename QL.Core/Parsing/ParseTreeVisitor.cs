using QL.Core.Ast;
using static QL.Core.QLParser;
using Antlr4.Runtime.Tree;
using QL.Core.Types;
using System;

namespace QL.Core.Parsing
{
    internal class ParseTreeVisitor : QLBaseVisitor<Node>
    {
        public override Node Visit(IParseTree tree)
        {
            if (tree == null)
            {
                return new NullNode();
            }

            return base.Visit(tree);
        }

        public override Node VisitForm(FormContext context)
        {
            ITerminalNode label = context.LABEL();
            var form = new FormNode(context.Start, label?.GetText());
            form.AddChild(Visit(context.block()));

            return form;
        }

        public override Node VisitBlock(BlockContext context)
        {
            var blockNode = new BlockNode(context.Start);
            foreach (StatementContext x in context.statement())
            {
                blockNode.AddChild(Visit(x));
            }

            return blockNode;
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
            
            var question = new QuestionNode(context.Start,
                context.STRING().GetText().Replace("\"", string.Empty),
                context.LABEL().GetText(),
                QLTypes.FromStringTypeToQLType(context.type().GetText()));
            question.AddChild(Visit(context.expression()));

            return question;
        }

        public override Node VisitConditional(ConditionalContext context)
        {
            var conditional = new ConditionalNode(context.Start);
            conditional.AddChild(Visit(context.expression()));
            conditional.AddChild(Visit(context.ifBlock()));
            conditional.AddChild(Visit(context.elseBlock()));

            return conditional;
        }

        public override Node VisitIfBlock(IfBlockContext context)
        {
            return Visit(context.block());
        }

        public override Node VisitElseBlock(ElseBlockContext context)
        {
            return Visit(context.block());
        }

        public override Node VisitVariableExpression(VariableExpressionContext context)
        {
            return new VariableNode(context.Start, context.LABEL().GetText());
        }

        public override Node VisitLiteralExpression(LiteralExpressionContext context)
        {
            QLType type = QLTypes.FromTokenTypeToQLType(context.Start);
            return new LiteralNode(context.Start, context.literal().GetText(), type);
        }

        public override Node VisitUnaryExpression(UnaryExpressionContext context)
        {
            var expression = new ExpressionNode(context.Start, context.unaryOperator().GetText());
            expression.AddChild(Visit(context.expression()));
            return expression;
        }

        public override Node VisitBinaryExpression(BinaryExpressionContext context)
        {
            var expression = new ExpressionNode(context.Start, context.binaryOperator().GetText());
            expression.AddChild(Visit(context.expression(0)));
            expression.AddChild(Visit(context.expression(1)));
            return expression;
        }

        public override Node VisitScopedExpresion(ScopedExpresionContext context)
        {
            return Visit(context.expression());
        }
    }
}
