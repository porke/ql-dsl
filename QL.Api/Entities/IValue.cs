namespace QL.Api.Entities
{
    public interface IValue
    {
        QLType Type { get; }

        bool ToBoolean();
        int ToInt();
        double ToDecimal();
        string ToString();
    }
}
