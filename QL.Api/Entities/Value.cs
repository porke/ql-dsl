using System;
using System.Collections.Generic;

namespace QL.Api.Entities
{
    public class Value
    {
        private static Dictionary<QLType, object> _defaultValues = new Dictionary<QLType, object>
        {   { QLType.Boolean, false },
            { QLType.Decimal, 0.0 },
            { QLType.Integer, 0 },
            { QLType.String, "" },
            { QLType.Undefined, new object() },
            { QLType.Date, DateTime.MinValue } };

        private object _value;
        private QLType _type;

        public Value(object value, QLType type)
        {
            _value = value;
            _type = type;
            // TODO it is still possible to assign a "23.4" to a QLInteger Value
        }

        /// <summary>
        /// Default value constructor
        /// </summary>
        public Value(QLType type) : this(_defaultValues[type], type)
        {
        }

        public QLType Type => _type;

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
    }
}
