using Presentation.ViewModels;
using QL.Api.Entities;
using System.Windows;
using System.Windows.Controls;
using QLS.Api.Entities;

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
            if (question.Widget == WidgetType.Checkbox)
            {
                return CheckboxDataTemplate;
            }
            if (question.Widget == WidgetType.Dropdown)
            {
                return DropdownDataTemplate;
            }
            if (question.Widget == WidgetType.Radio)
            {
                return RadioDataTemplate;
            }

            if (question.Widget == WidgetType.Spinbox)
            {
                if (question.QLType == QLType.Integer)
                {
                    return IntegerDataTemplate;
                }                    
                if (question.QLType == QLType.Decimal)
                {
                    return DecimalDataTemplate;
                }
            }

            return TextDataTemplate;
        }
    }
}
