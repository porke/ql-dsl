using Antlr4.Runtime.Misc;
using QLS.Api.Ast;
using static QLS.Core.QLSParser;

namespace QLS.Core.Parsing
{
    internal class ParseTreeVisitor : QLSBaseVisitor<Node>
    {
        public override Node VisitStylesheet([NotNull] StylesheetContext context)
        {
            var stylesheet = new StylesheetNode(context.Start, context.LABEL().GetText());

            foreach (PageContext page in context.page())
            {
                stylesheet.AddChild(Visit(page));
            }

            return stylesheet;
        }

        public override Node VisitPage([NotNull] PageContext context)
        {
            return new PageNode(context.Start, context.LABEL().GetText());
        }
    }
}
