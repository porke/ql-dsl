using System;
using System.Collections.Generic;
using QL.Api.Entities;
using QL.Api.Factories;

namespace QL.Core.Interpreting
{
    public class Value : IValue
    {
        private static Dictionary<QLType, object> _defaultValues = new Dictionary<QLType, object>
        {   { QLType.Boolean, false },
            { QLType.Decimal, 0.0 },
            { QLType.Integer, 0 },
            { QLType.String, "" },
            { QLType.Undefined, new object() },
            { QLType.Date, DateTime.MinValue } };

        private object _value;

        private Value(object value, QLType type)
        {
            _value = value;
            Type = type;
        }

        private Value(QLType type) : this(_defaultValues[type], type)
        {
        }

        public QLType Type { get; }

        public bool ToBoolean()
        {
            return bool.Parse(_value.ToString());
        }

        public int ToInt()
        {
            return int.Parse(_value.ToString());
        }

        public double ToDecimal()
        {
            return double.Parse(_value.ToString());
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        internal class ValueFactory : IValueFactory
        {
            public IValue CreateDefaultValue(QLType type)
            {
                return new Value(type);
            }

            public IValue CreateValue(object value, QLType type)
            {
                switch (type)
                {
                    case QLType.Boolean: return new Value((bool)value, type);
                    case QLType.String: return new Value((string)value, type);
                    case QLType.Integer: return new Value(Convert.ToInt32(value), type);
                    case QLType.Decimal: return new Value(Convert.ToDouble(value), type);
                    case QLType.Date: return new Value((string)value, type);
                }
                throw new NotSupportedException("Tried to create a Value of a type that is not implemented.");
            }

            public IValue CreateValueFromString(object value, QLType type)
            {
                switch (type)
                {
                    case QLType.Boolean: return new Value(bool.Parse(value.ToString()), type);
                    case QLType.String: return new Value(value.ToString(), type);
                    case QLType.Integer: return new Value(int.Parse(value.ToString()), type);
                    case QLType.Decimal: return new Value(double.Parse(value.ToString()), type);
                    case QLType.Date: return new Value(value.ToString(), type);
                }
                throw new NotSupportedException("Tried to create a Value of a type that is not implemented.");
            }
        }
    }
}
