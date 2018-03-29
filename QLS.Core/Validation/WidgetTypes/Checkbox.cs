using QL.Api.Entities;
using QLS.Api.Entities;

namespace QLS.Core.Validation.WidgetTypes
{
    public class Checkbox : IWidget
    {
        public WidgetType Type => WidgetType.Checkbox;

        public bool IsCompatibleWith(QLType questionType)
        {
            return questionType == QLType.Boolean;
        }

        public override string ToString()
        {
            return WidgetType.Checkbox.ToString();
        }
    }
}
