namespace QL.Core.Types
{
    public class Value
    {
        private object _value;

        public Value(object value)
        {
            _value = value;
        }

        public bool ToBool()
        {
            return (bool)_value;
        }

        public int ToInt()
        {
            return (int)_value;
        }
    }
}
