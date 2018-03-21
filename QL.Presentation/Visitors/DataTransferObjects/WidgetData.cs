using QLS.Api.Entities;

namespace Presentation.Visitors.DataTransferObjects
{
    internal class WidgetData
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
}
