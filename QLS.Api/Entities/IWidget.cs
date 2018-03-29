using QL.Api.Entities;

namespace QLS.Api.Entities
{
    public interface IWidget
    {
        WidgetType Type { get; }
        bool IsCompatibleWith(QLType questionType);
        string ToString();
    }
}