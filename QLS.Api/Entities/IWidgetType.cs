using QL.Api.Entities;

namespace QLS.Api.Entities
{
    public interface IWidgetType
    {
        bool Compatible(QLType questionType);
        string ToString();
    }
}