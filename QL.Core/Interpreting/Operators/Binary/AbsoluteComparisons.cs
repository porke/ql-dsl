using QL.Api.Entities;
using QL.Api.Operators;
using System;

namespace QL.Core.Interpreting.Operators
{
    internal class AbsoluteComparison : IOperator
    {
        public delegate bool Opperator(string i, string j);
        private Opperator _opperator;
        private string _asString;

        public AbsoluteComparison(Opperator opperator, string asString)
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

        public bool AcceptTypes(QLType leftHand, QLType rightHand)
        {
            if ((leftHand == QLType.Integer || leftHand == QLType.Decimal)
                && (rightHand == QLType.Integer || rightHand == QLType.Decimal))
            {
                return true;
            }
            return (leftHand == rightHand);
        }

        public Value Evaluate(Value leftHand, Value rightHand)
        {
            return new Value(string.Equals(leftHand.ToString(), rightHand.ToString(), StringComparison.Ordinal), QLType.Boolean);
        }

        public QLType ResultingType(QLType leftType, QLType rightType) {
            return QLType.Boolean;
        }

    }
}
