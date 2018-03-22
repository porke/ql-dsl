using QL.Api.Entities;
using QLS.Api.Entities;

namespace QLS.Core.Validation.WidgetTypes
{
    public class Textbox : IWidgetType
    {
        public bool Compatible(QLType questionType)
        {
            return (questionType == QLType.Integer)
                || (questionType == QLType.Decimal)
                || (questionType == QLType.String);
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
