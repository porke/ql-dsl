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
            foreach (Section_contentsContext section_content in context.section_contents())
            {
                if (section_content.question() != null)
                {
                    section.AddChild(Visit(section_content.question()));
                }

                if (section_content.default_style() != null)
                {
                    section.AddChild(Visit(section_content.default_style()));
                }
            }            

            return section;
        }

        public override Node VisitQuestion([NotNull] QuestionContext context)
        {
            var questionNode = new QuestionNode(context.Start, context.LABEL().ToString());
            if (context.style() != null)
            {
                questionNode.AddChild(Visit(context.style()));
            }

            return questionNode;
        }

        public override Node VisitWidget_type([NotNull] Widget_typeContext context)
        {
            if (context.option_widget() != null)
            {
                var widgetNode = new WidgetNode(context.Start, QLSWidgetTypeConverter.FromTokenToWidgetType(context.Start));
                if (context.option().Length == 2)
                {
                    widgetNode.AddChild(Visit(context.option(0)));
                    widgetNode.AddChild(Visit(context.option(1)));
                }
                return widgetNode;
            }

            return new WidgetNode(context.Start, QLSWidgetTypeConverter.FromTokenToWidgetType(context.Start));
        }

        public override Node VisitOption([NotNull] OptionContext context)
        {
            return new WidgetOptionNode(context.Start, context.STRING().GetText().Trim('\"'));
        }

        public override Node VisitWidget([NotNull] WidgetContext context)
        {
            return Visit(context.widget_type());
        }

        public override Node VisitStyle([NotNull] StyleContext context)
        {            
            var styleNode = new StyleNode(context.Start, string.Empty);
            foreach (PropertyContext property in context.property())
            {
                styleNode.AddChild(Visit(property));
            }

            if (context.widget() != null)
            {
                styleNode.AddChild(Visit(context.widget()));
            }

            return styleNode;
        }

        public override Node VisitDefault_style([NotNull] Default_styleContext context)
        {
            var defaultStyle = new StyleNode(context.Start, context.type().GetText());
            defaultStyle.AddChild(Visit(context.style()));
            return defaultStyle;
        }

        public override Node VisitWidget_property([NotNull] Widget_propertyContext context)
        {
            return Visit(context.widget());
        }

        public override Node VisitColor_property([NotNull] Color_propertyContext context)
        {
            return new PropertyNode(context.start, context.LABEL().GetText(), context.COLOR().GetText());
        }

        public override Node VisitString_property([NotNull] String_propertyContext context)
        {
            return new PropertyNode(context.start, context.LABEL().GetText(), context.STRING().GetText().Trim('\"'));
        }

        public override Node VisitInteger_property([NotNull] Integer_propertyContext context)
        {
            return new PropertyNode(context.start, context.LABEL().GetText(), context.INTEGER().GetText());
        }
    }
}
