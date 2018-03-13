using QL.Api.Entities;
using QL.Api.Operators;

namespace QL.Core.Interpreting.Operators
{
    internal class RelativeComparison : IOperator
    {
        public delegate bool Opperator(double i, double j);
        private Opperator _opperator;
        private string _asString;

        public RelativeComparison(Opperator opperator, string asString)
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
            return ((leftHand == QLType.Integer || leftHand == QLType.Decimal)
                && (rightHand == QLType.Integer || rightHand == QLType.Decimal));
        }

        public Value Evaluate(Value leftHand, Value rightHand)
        {
            return new Value(_opperator(leftHand.ToDecimal(), rightHand.ToDecimal()), QLType.Boolean);
        }

        public QLType ResultingType(QLType leftType, QLType rightType)
        {
            return QLType.Boolean;
        }
    }
}
