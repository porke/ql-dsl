using QL.Api.Operators;
using QL.Api.Types;
using System;

namespace QL.Core.Interpreting.Operators
{
    internal class Arithmetic : IOperator
    {
        public delegate double Opperator(double i, double j);
        private Opperator _opperator;
        private string _asString;

        public Arithmetic(Opperator opperator, string asString)
        {
            _opperator = opperator;
            _asString = asString;
        }

        public string AsString
        {
            get
            {
                return _asString;
            }
        }

        public Value Evaluate(Value leftHand, Value rightHand)
        {
            QLType finalType = ResultingType(leftHand.Type, rightHand.Type);
            return new Value(_opperator(leftHand.ToDecimal(), rightHand.ToDecimal()), finalType);
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
