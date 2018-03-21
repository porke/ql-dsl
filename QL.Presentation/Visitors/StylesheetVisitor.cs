using Presentation.ViewModels;
using QLS.Api.Ast;
using System.Linq;
using System.Collections.Generic;
using QLS.Api.Entities;
using Presentation.Properties;
using System.Windows.Media;

namespace Presentation.Visitors
{
    internal class StylesheetVisitor : BaseVisitor<object>
    {
        private class WidgetData
        {
            public WidgetData(WidgetType type, string yesOption, string noOption)
            {
                WidgetType = type;
                YesOption = yesOption;
                NoOption = noOption;
            }

            public WidgetType WidgetType { get; }
            public string YesOption { get; }
            public string NoOption { get; }
        }

        private class PropertyData
        {
            public PropertyData(string name, string value)
            {
                Name = name;
                Value = value;
            }
               
            public string Name { get; }
            public string Value { get; }
        }

        private class StyleData
        {
            public StyleData(IReadOnlyList<PropertyData> properties, WidgetData widget, string targetType)
            {
                Properties = properties;
                Widget = widget;
                TargetType = targetType;
            }

            public string TargetType { get; }
            public IReadOnlyList<PropertyData> Properties { get; }
            public WidgetData Widget { get; }
        }

        private IReadOnlyList<QuestionViewModel> _questions;

        public PagesViewModel PagesViewModel { get; private set; } = new PagesViewModel();

        public StylesheetVisitor(IReadOnlyList<QuestionViewModel> questions)
        {
            _questions = questions;
        }

        public override object Visit(PageNode page)
        {
            var pageViewModel = new PageViewModel(page.Label);
            PagesViewModel.Pages.Add(pageViewModel);
            foreach (var sectionNode in page.ChildNodes)
            {
                pageViewModel.Sections.Sections.Add(sectionNode.Accept(this) as SectionViewModel);
            }
            
            return null;
        }

        public override object Visit(SectionNode section)
        {            
            var sectionViewModel = new SectionViewModel(section.Label);
            foreach (var styleNode in section.ChildNodes.OfType<StyleNode>())
            {
                var styleData = styleNode.Accept(this) as StyleData;
                // TODO convert to view model?
            }

            foreach (var questionNode in section.ChildNodes.OfType<QuestionNode>())
            {
                var questionViewModel = questionNode.Accept(this) as QuestionViewModel;
                if (questionViewModel != null)
                {
                    sectionViewModel.Questions.Add(questionViewModel);
                }                
            }

            return sectionViewModel;
        }

        public override object Visit(QuestionNode question)
        {
            QuestionViewModel questionVm = _questions.FirstOrDefault(q => q.Id.Equals(question.Label));
            if (questionVm == null)
            {
                return null;
            }

            var styleNode = question.ChildNodes.OfType<StyleNode>().FirstOrDefault();
            if (styleNode != null)
            {
                var styleData = styleNode.Accept(this) as StyleData;
                // TODO: decorate the question
                // Convert style data to view model
                //int widthProperty = int.Parse(styleData.Properties.First(x => x.Name == "width").Value);
                //int fontSize = int.Parse(styleData.Properties.First(x => x.Name == "fontsize").Value);
                Color color = (Color)ColorConverter.ConvertFromString(styleData.Properties.First(x => x.Name == "color").Value);
                //string font = styleData.Properties.First(x => x.Name == "font").Value;
                questionVm.Style = new StyleViewModel(11, 400, "Arial", color);

                questionVm.WidgetType = styleData.Widget.WidgetType;
            }

            return questionVm;
        }

        public override object Visit(PropertyNode node)
        {
            return new PropertyData(node.Name, node.Value);
        }

        public override object Visit(WidgetNode node)
        {
            var options = new List<string>();
            foreach (var option in node.ChildNodes)
            {
                options.Add(option.Accept(this).ToString());
            }

            return new WidgetData(node.WidgetType, options.FirstOrDefault() ?? Resources.Yes, options.Skip(1).FirstOrDefault() ?? Resources.No);
        }

        public override object Visit(WidgetOptionNode node)
        {
            return node.Label;
        }

        public override object Visit(StyleNode node)
        {
            var properties = new List<PropertyData>();
            foreach (var property in node.ChildNodes.OfType<PropertyNode>())
            {
                properties.Add(property.Accept(this) as PropertyData);
            }

            var widgetNode = node.ChildNodes.OfType<WidgetNode>().FirstOrDefault();
            WidgetData widgetData = new WidgetData(WidgetType.Undefined, Resources.Yes, Resources.No);
            if (widgetNode != null)
            {
                widgetData = widgetNode.Accept(this) as WidgetData;
            }
            
            return new StyleData(properties, widgetData, node.TargetType);
        }
    }
}
