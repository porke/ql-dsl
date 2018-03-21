using System.Collections.Generic;

namespace Presentation.Visitors.DataTransferObjects
{
    internal class StyleData
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
}
