using QL.Api.Entities;
using QLS.Api.Entities;

namespace QLS.Core.Validation.WidgetTypes
{
    public class Spinbox : IWidget
    {
        public WidgetType Type => WidgetType.Spinbox;

        public bool IsCompatibleWith(QLType questionType)
        {
            return questionType == QLType.Integer || questionType == QLType.Decimal;
        }

        public override string ToString()
        {
            return WidgetType.Spinbox.ToString();
        }
    }
}
