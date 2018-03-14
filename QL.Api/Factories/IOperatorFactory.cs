using QL.Api.Operators;

namespace QL.Api.Factories
{
    public interface IOperatorFactory
    {
        IOperator CreateBinaryOperator(string text);
        IOperator CreateUnaryOperator(string text);
    }
}
