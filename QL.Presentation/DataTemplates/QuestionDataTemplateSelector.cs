using Presentation.ViewModels;
using QL.Api.Entities;
using QLS.Api.Entities;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.DataTemplates
{
    internal class QuestionDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CheckboxDataTemplate { get; set; }
        public DataTemplate DropdownDataTemplate { get; set; }
        public DataTemplate RadioDataTemplate { get; set; }
        public DataTemplate TextDataTemplate { get; set; }
        public DataTemplate IntegerDataTemplate { get; set; }
        public DataTemplate DecimalDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var question = item as QuestionViewModel;
            switch (question.WidgetType)
            {
                case WidgetType.Checkbox:
                    return CheckboxDataTemplate;
                case WidgetType.Dropdown:
                    return DropdownDataTemplate;
                case WidgetType.Radio:
                    return RadioDataTemplate;
                case WidgetType.Textbox:
                    return TextDataTemplate;
            }

            if (question.QLType == QLType.Integer)
            {
                return IntegerDataTemplate;
            }

            if (question.QLType == QLType.Decimal)
            {
                return DecimalDataTemplate;
            }

            return TextDataTemplate;
        }
    }
}
