using QL.Api.Entities;
using QL.Api.Factories;
using QL.Api.Operators;

namespace QL.Core.Interpreting.Operators.Binary
{
    internal class Logical : IOperator
    {
        public delegate bool Opperator(bool i, bool j);
        private Opperator _opperator;
        private string _asString;
        private readonly IValueFactory _valueFactory;

        internal Logical(Opperator opperator, string asString, IValueFactory valueFactory)
        {
            _opperator = opperator;
            _asString = asString;
            _valueFactory = valueFactory;
        }

        public override string ToString()
        {
            return _asString;
        }

        public bool AcceptTypes(QLType leftHand, QLType rightHand)
        {
            return (leftHand == QLType.Boolean && rightHand == QLType.Boolean);
        }

        public IValue Evaluate(IValue leftHand, IValue rightHand)
        {
            return _valueFactory.CreateValue(_opperator(leftHand.ToBoolean(), rightHand.ToBoolean()), QLType.Boolean);
        }

        public QLType ResultingType(QLType leftType, QLType rightType)
        {
            return QLType.Boolean;
        }
    }
}
