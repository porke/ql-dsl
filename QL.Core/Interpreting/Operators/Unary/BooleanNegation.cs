using QL.Api.Entities;
using QL.Api.Factories;
using QL.Api.Operators;

namespace QL.Core.Interpreting.Operators
{
    internal class BooleanNegation : IOperator
    {
        private string _asString;
        private readonly IValueFactory _valueFactory;

        internal BooleanNegation(string asString, IValueFactory valueFactory)
        {
            _asString = asString;
            _valueFactory = valueFactory;
        }

        public override string ToString()
        {
            return _asString;
        }

        public IValue Evaluate(IValue input, IValue empty = null)
        {
            return _valueFactory.CreateValue(!input.ToBoolean(), QLType.Boolean);
        }

        public bool AcceptTypes(QLType value, QLType empty = QLType.Undefined)
        {
            return (value == QLType.Boolean);
        }

        public QLType ResultingType(QLType value, QLType empty = QLType.Undefined)
        {
            return QLType.Boolean;
        }
    }
}
