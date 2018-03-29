using QL.Api.Entities;
using QLS.Api.Entities;

namespace QLS.Core.Validation.WidgetTypes
{
    public class Textbox : IWidget
    {
        public WidgetType Type => WidgetType.Textbox;

        public bool IsCompatibleWith(QLType questionType)
        {
            return questionType == QLType.Integer
                || questionType == QLType.Decimal
                || questionType == QLType.String;
        }

        public override string ToString()
        {
            return WidgetType.Textbox.ToString();
        }
    }
}
