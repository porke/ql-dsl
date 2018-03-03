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
            return bool.Parse(_value.ToString());
        }

        public int ToInt()
        {
            return int.Parse(_value.ToString());
        }
    }
}
