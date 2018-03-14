namespace QL.Api.Entities
{
    public interface IValue
    {
        QLType GetType();
        bool ToBoolean();
        int ToInt();
        double ToDecimal();
        string ToString();
    }
}
