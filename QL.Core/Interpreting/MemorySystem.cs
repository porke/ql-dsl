using QL.Core.Types;
using System.Collections.Generic;

namespace QL.Core.Interpreting
{
    public class MemorySystem
    {
        private Dictionary<string, Value> _values = new Dictionary<string, Value>();

        public Value RetrieveValue(string id)
        { 
            Value returnValue;
            if (!_values.TryGetValue(id, out returnValue))
            {
                returnValue = new Value(0);
                AssignValue(id, returnValue);
            }

            return returnValue;
        }

        public void AssignValue(string id, Value value)
        {
            _values[id] = value;
        }
    }
}
