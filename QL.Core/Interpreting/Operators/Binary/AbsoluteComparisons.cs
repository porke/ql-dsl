using System;
using QL.Api.Entities;
using QL.Api.Factories;
using QL.Api.Operators;

namespace QL.Core.Interpreting.Operators.Binary
{
    internal class AbsoluteComparison : IOperator
    {
        public delegate bool Operator(string i, string j);
        private Operator _operator;
        private string _asString;
        private readonly IValueFactory _valueFactory;

        internal AbsoluteComparison(Operator @operator, string asString, IValueFactory valueFactor)
        {
            _operator = @operator;
            _asString = asString;
            _valueFactory = valueFactor;
        }

        public override string ToString()
        {
            return _asString;
        }

        public bool AcceptTypes(QLType leftHand, QLType rightHand)
        {
            if ((leftHand == QLType.Integer || leftHand == QLType.Decimal)
                && (rightHand == QLType.Integer || rightHand == QLType.Decimal))
            {
                return true;
            }
            return leftHand == rightHand;
        }

        public IValue Evaluate(IValue leftHand, IValue rightHand)
        {
            return _valueFactory.CreateValue(string.Equals(leftHand.ToString(), rightHand.ToString(), StringComparison.Ordinal), QLType.Boolean);
        }

        public QLType ResultingType(QLType leftType, QLType rightType)
        {
            return QLType.Boolean;
        }
    }
}
