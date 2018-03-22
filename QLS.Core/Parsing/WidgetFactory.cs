using Antlr4.Runtime;
using QLS.Api.Entities;
using QLS.Core.Validation.WidgetTypes;

namespace QLS.Core.Parsing
{
    internal static class WidgetFactory
    {
        public static IWidgetType FromTokenToWidgetType(IToken token)
        {
            switch (token.Type)
            {
                case QLSParser.WIDGETDROPDOWN:
                    return new Dropdown();
                case QLSParser.WIDGETCHECKBOX:
                    return new Checkbox();
                case QLSParser.WIDGETRADIO:
                    return new Radio();
                case QLSParser.WIDGETTEXTBOX:
                    return new Textbox();
                case QLSParser.WIDGETSPINBOX:
                    return new Spinbox();
            }

            return new Textbox();
        }
    }
}
