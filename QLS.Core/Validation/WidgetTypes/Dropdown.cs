using QL.Api.Entities;
using QLS.Api.Entities;

namespace QLS.Core.Validation.WidgetTypes
{
    public class Dropdown : IWidget
    {
        public WidgetType Type => WidgetType.Dropdown;

        public bool IsCompatibleWith(QLType questionType)
        {
            return questionType == QLType.Boolean;
        }

        public override string ToString()
        {
            return WidgetType.Dropdown.ToString();
        }
    }
}
