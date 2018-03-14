using Antlr4.Runtime.Misc;
using QLS.Api.Ast;
using QLS.Api.Entities;
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
            var page = new PageNode(context.Start, context.LABEL().GetText());
            foreach (SectionContext section in context.section())
            {
                page.AddChild(Visit(section));
            }

            return page;
        }

        public override Node VisitSection([NotNull] SectionContext context)
        {
            var section = new SectionNode(context.Start, context.STRING().ToString().Trim('"'));
            foreach (QuestionContext question in context.question())
            {
                section.AddChild(Visit(question));
            }

            return section;
        }

        public override Node VisitQuestion([NotNull] QuestionContext context)
        {
            var questionNode = new QuestionNode(context.Start, context.LABEL().ToString());
            if (context.widget() != null)
            {
                questionNode.AddChild(Visit(context.widget()));
            }
            return questionNode;
        }

        public override Node VisitWidget([NotNull] WidgetContext context)
        {
            WidgetType widgetType = WidgetType.Textbox;
            switch (context.widget_type().Start.Type)
            {
                case WIDGETDROPDOWN:
                    widgetType = WidgetType.Dropdown;
                    break;
                case WIDGETCHECKBOX:
                    widgetType = WidgetType.Checkbox;
                    break;
                case WIDGETRADIO:
                    widgetType = WidgetType.Radio;
                    break;
            }
            return new WidgetNode(context.Start, widgetType);
        }
    }
}
