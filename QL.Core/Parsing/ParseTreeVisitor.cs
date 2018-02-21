using QL.Core.AST;
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
            var form = new FormNode(context.Start, context.LABEL().GetText());
            form.AddChild(Visit(context.block()));

            return form;            
        }

        public override Node VisitBlock(BlockContext context)
        {            
            var blockNode = new Node(context.Start);
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
                context.description().STR().GetText().Replace("\"", string.Empty),
                context.name().LABEL().GetText(),
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
            return new NullNode();
        }

        public override Node VisitExpression(ExpressionContext context)
        {
            return new NullNode();
        }

        public override Node VisitLiteralExpression(LiteralExpressionContext context)
        {
            return new LiteralNode(context.Start, context.literal().GetText());
        }
    }
}
