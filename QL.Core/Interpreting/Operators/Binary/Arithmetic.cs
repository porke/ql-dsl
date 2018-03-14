using QL.Api.Entities;
using QL.Api.Factories;
using QL.Api.Operators;

namespace QL.Core.Interpreting.Operators
{
    internal class Arithmetic : IOperator
    {
        public delegate double Opperator(double i, double j);
        private Opperator _opperator;
        private string _asString;
        private readonly IValueFactory _valueFactory;

        internal Arithmetic(Opperator opperator, string asString, IValueFactory valueFactory)
        {
            _opperator = opperator;
            _asString = asString;
            _valueFactory = valueFactory;
        }

        public override string ToString()
        {
                return _asString;
        }

        public IValue Evaluate(IValue leftHand, IValue rightHand)
        {
            QLType finalType = ResultingType(leftHand.GetType(), rightHand.GetType());
            return _valueFactory.CreateValue(_opperator(leftHand.ToDecimal(), rightHand.ToDecimal()), finalType);
        }

        public bool AcceptTypes(QLType leftHand, QLType rightHand)
        {
            return ((leftHand == QLType.Integer || leftHand == QLType.Decimal)
                && (rightHand == QLType.Integer || rightHand == QLType.Decimal));
        }

        public QLType ResultingType(QLType leftType, QLType rightType)
        {
            if (leftType == rightType)
            {
                return leftType;
            }
            return QLType.Decimal;
        }
    }
}
