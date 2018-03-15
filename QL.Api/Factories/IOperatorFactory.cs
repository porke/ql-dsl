using Antlr4.Runtime;
using QL.Api.Operators;

namespace QL.Api.Factories
{
    public interface IOperatorFactory
    {
        IOperator CreateBinaryOperator(IToken token);
        IOperator CreateUnaryOperator(IToken token);
    }
}
