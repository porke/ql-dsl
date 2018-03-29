using QL.Api.Entities;
using QLS.Api.Entities;

namespace QLS.Core.Validation.WidgetTypes
{
    public class Undefined : IWidget
    {
        public WidgetType Type => WidgetType.Undefined;

        public bool IsCompatibleWith(QLType questionType)
        {
            return true;
        }

        public override string ToString()
        {
            return WidgetType.Undefined.ToString();
        }
    }
}
