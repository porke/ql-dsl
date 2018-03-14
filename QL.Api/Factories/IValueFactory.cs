using QL.Api.Entities;

namespace QL.Api.Factories
{
    public interface IValueFactory
    {
        IValue CreateValue(object value, QLType type);
        IValue CreateValueFromString(object value, QLType type);
        IValue CreateDefaultValue(QLType type);
    }
}
