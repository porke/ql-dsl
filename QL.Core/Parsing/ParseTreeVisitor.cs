using static QL.Core.QLParser;
using Antlr4.Runtime.Tree;
using QL.Api.Ast;
using QL.Api.Factories;

namespace QL.Core.Parsing
{
    internal class ParseTreeVisitor : QLBaseVisitor<Node>
    {
        private readonly IOperatorFactory _operatorFactory;
        private int _blockDepth;

        internal ParseTreeVisitor(IOperatorFactory operatorFactory)
        {
            _operatorFactory = operatorFactory;
        }

        public override Node Visit(IParseTree tree)
        {
            if (tree == null)
            {
                return null;
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
            _blockDepth++;

            var blockNode = new BlockNode(context.Start, _blockDepth);
            foreach (StatementContext x in context.statement())
            {
                blockNode.AddChild(Visit(x));
            }

            _blockDepth--;
            return blockNode;
        }

        public override Node VisitStatement(StatementContext context)
        {
            QuestionContext question = context.question();
            ConditionalContext conditional = context.conditional();
            return question != null ? Visit(question) : Visit(conditional);
        }

        public override Node VisitQuestion(QuestionContext context)
        {
            var expressionNode = Visit(context.expression());
            var question = new QuestionNode(context.Start,
                context.STRING().GetText().Replace("\"", string.Empty),
                context.LABEL().GetText(),
                expressionNode != null,
                QLTypeConverter.FromStringTypeToQLType(context.type().GetText()));
            question.AddChild(expressionNode);

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
            return new LiteralNode(context.Start, context.literal().GetText(), QLTypeConverter.FromTokenTypeToQLType(context.Start));
        }

        public override Node VisitUnaryExpression(UnaryExpressionContext context)
        {
            var expression = new ExpressionNode(context.Start, _operatorFactory.CreateUnaryOperator(context.unaryOperator().Start));
            expression.AddChild(Visit(context.expression()));
            return expression;
        }

        public override Node VisitBinaryExpression(BinaryExpressionContext context)
        {
            var expression = new ExpressionNode(context.Start, _operatorFactory.CreateBinaryOperator(context.binaryOperator().Start));
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
