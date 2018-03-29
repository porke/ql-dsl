using QL.Api.Entities;
using QL.Api.Factories;
using QL.Api.Operators;

namespace QL.Core.Interpreting.Operators.Binary
{
    internal class Arithmetic : IOperator
    {
        public delegate double Operator(double i, double j);
        private Operator _operator;
        private string _asString;
        private readonly IValueFactory _valueFactory;

        internal Arithmetic(Operator @operator, string asString, IValueFactory valueFactory)
        {
            _operator = @operator;
            _asString = asString;
            _valueFactory = valueFactory;
        }

        public override string ToString()
        {
            return _asString;
        }

        public IValue Evaluate(IValue leftHand, IValue rightHand)
        {
            QLType finalType = ResultingType(leftHand.Type, rightHand.Type);
            return _valueFactory.CreateValue(_operator(leftHand.ToDecimal(), rightHand.ToDecimal()), finalType);
        }

        public bool AcceptTypes(QLType leftHand, QLType rightHand)
        {
            return (leftHand == QLType.Integer || leftHand == QLType.Decimal)
                   && (rightHand == QLType.Integer || rightHand == QLType.Decimal);
        }

        public QLType ResultingType(QLType leftType, QLType rightType)
        {
            return leftType == rightType ? leftType : QLType.Decimal;
        }
    }
}
