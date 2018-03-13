using System.Collections.Generic;

namespace QL.Api.Entities
{
    public sealed class MemorySystem
    {
        private Dictionary<string, Value> _values = new Dictionary<string, Value>();

        public bool TryRetrieveValue(string id, out Value returnValue)
        { 
            if (!_values.TryGetValue(id, out returnValue))
            {
                return false;
            }

            return true;
        }

        public void AssignValue(string id, Value value)
        {
            _values[id] = value;
        }
    }
}
