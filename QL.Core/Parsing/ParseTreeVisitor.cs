﻿using QL.Core.Ast;
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
            var blockNode = new EmptyNode(context.Start);
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
                context.type().GetText());
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
            var variable = new VariableNode(context.Start, context.LABEL().GetText());
            return variable;
        }

        public override Node VisitLiteralExpression(LiteralExpressionContext context)
        {
            var literal = new LiteralNode(context.Start, context.literal().GetText());
            return literal;
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