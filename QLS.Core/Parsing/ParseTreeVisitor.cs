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

                if (section_content.style() != null)
                {
                    section.AddChild(Visit(section_content.style()));
                }
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

            if (context.property() != null)
            {
                foreach (PropertyContext property in context.property())
                {
                    questionNode.AddChild(Visit(property));
                }
            }

            return questionNode;
        }

        public override Node VisitWidget([NotNull] WidgetContext context)
        {
            return new WidgetNode(context.Start, QLSWidgetTypeConverter.FromTokenToWidgetType(context.widget_type().Start));
        }

        public override Node VisitStyle([NotNull] StyleContext context)
        {
            var styleNode = new StyleNode(context.Start, context.type().GetText());
            foreach (PropertyContext property in context.property())
            {
                styleNode.AddChild(Visit(property));
            }

            return styleNode;
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
