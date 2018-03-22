using Presentation.ViewModels;
using QL.Api.Entities;
using QLS.Core.Validation.WidgetTypes;
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
            if (question.WidgetType is Checkbox)
            {
                return CheckboxDataTemplate;
            } else if (question.WidgetType is Dropdown)
            {
                return DropdownDataTemplate;
            } else if (question.WidgetType is Radio)
            {
                return RadioDataTemplate;
            } else if (question.WidgetType is Textbox)
            {
                return TextDataTemplate;
            } else if (question.QLType == QLType.Integer)
            {
                return IntegerDataTemplate;
            } else if (question.QLType == QLType.Decimal)
            {
                return DecimalDataTemplate;
            }

            return TextDataTemplate;
        }
    }
}
