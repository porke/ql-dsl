using QLS.Api.Entities;

namespace Presentation.Visitors.DataTransferObjects
{
    internal class WidgetData
    {
        public WidgetData(IWidget type, string yesOption, string noOption)
        {
            Widget = type;
            YesOption = yesOption;
            NoOption = noOption;
        }

        public IWidget Widget { get; }
        public string YesOption { get; }
        public string NoOption { get; }
    }
}
