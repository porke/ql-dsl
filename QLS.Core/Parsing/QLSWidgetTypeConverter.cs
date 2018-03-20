using Antlr4.Runtime;
using QLS.Api.Entities;

namespace QLS.Core.Parsing
{
    internal static class QLSWidgetTypeConverter
    {
        public static WidgetType FromTokenToWidgetType(IToken token)
        {
            switch (token.Type)
            {
                case QLSParser.WIDGETDROPDOWN:
                    return WidgetType.Dropdown;
                case QLSParser.WIDGETCHECKBOX:
                    return WidgetType.Checkbox;
                case QLSParser.WIDGETRADIO:
                    return WidgetType.Radio;
                case QLSParser.WIDGETTEXTBOX:
                    return WidgetType.Textbox;
                case QLSParser.WIDGETSPINBOX:
                    return WidgetType.Spinbox;
            }

            return WidgetType.Textbox;
        }
    }
}
