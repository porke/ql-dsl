using QL.Api.Entities;
using QLS.Api.Entities;

namespace QLS.Core.Validation.WidgetTypes
{
    public class Radio : IWidget
    {
        public WidgetType Type => WidgetType.Radio;

        public bool IsCompatibleWith(QLType questionType)
        {
            return questionType == QLType.Boolean;
        }

        public override string ToString()
        {
            return WidgetType.Radio.ToString();
        }
    }
}
