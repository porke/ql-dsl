using Presentation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.DataTemplates
{
    internal class FormDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SinglePageFormDataTemplate { get; set; }
        public DataTemplate MultiPageFormDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is MultiPageFormViewModel)
            {
                return MultiPageFormDataTemplate;
            }

            return SinglePageFormDataTemplate;
        }
    }
}
