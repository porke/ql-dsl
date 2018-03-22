using QL.Api.Entities;
using QLS.Api.Entities;

namespace QLS.Core.Validation.WidgetTypes
{
    public class Checkbox : IWidgetType
    {
        public bool Compatible(QLType questionType)
        {
            return questionType == QLType.Boolean;
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
