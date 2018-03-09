using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QL.Api.Entities
{
    public class SymbolTable : IEnumerable<Symbol>
    {
        private IList<Symbol> _symbols = new List<Symbol>();

        public void Add(Symbol symbol)
        {
            _symbols.Add(symbol);
        }

        public IEnumerator<Symbol> GetEnumerator()
        {
            return _symbols.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _symbols.Count;

        public Symbol this[string name]
        {
            get
            {
                return _symbols.FirstOrDefault(symbol => symbol.Name == name);
            }
        }

        public Symbol this[int index]
        {
            get
            {
                return _symbols[index];
            }
        }
    }
}
