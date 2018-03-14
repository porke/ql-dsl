using System.Collections.Generic;

namespace QL.Api.Entities
{
    public sealed class MemorySystem
    {
        private Dictionary<string, IValue> _values = new Dictionary<string, IValue>();

        public bool TryRetrieveValue(string id, out IValue returnValue)
        { 
            if (!_values.TryGetValue(id, out returnValue))
            {
                return false;
            }

            return true;
        }

        public void AssignValue(string id, IValue value)
        {
            _values[id] = value;
        }
    }
}
