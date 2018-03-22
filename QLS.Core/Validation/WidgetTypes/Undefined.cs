using QL.Api.Entities;
using QLS.Api.Entities;

namespace QLS.Core.Validation.WidgetTypes
{
    public class Undefined : IWidgetType
    {
        public bool Compatible(QLType questionType)
        {
            return true;
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
