using QL.Api.Entities;
using QL.Api.Operators;

namespace QL.Core.Interpreting.Operators
{
    internal class Logical : IOperator
    {
        public delegate bool Opperator(bool i, bool j);
        private Opperator _opperator;
        private string _asString;

        public Logical(Opperator opperator, string asString)
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
            return (leftHand == QLType.Boolean && rightHand == QLType.Boolean);
        }

        public Value Evaluate(Value leftHand, Value rightHand)
        {
            return new Value(_opperator(leftHand.ToBoolean(), rightHand.ToBoolean()), QLType.Boolean);
        }

        public QLType ResultingType(QLType leftType, QLType rightType)
        {
            return QLType.Boolean;
        }
    }
}
