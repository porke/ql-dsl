using System.Collections.Generic;

namespace QL.Core.Symbols
{
    public class SymbolTable
    {
        private IList<Symbol> _symbols = new List<Symbol>();

        public void Add(Symbol symbol)
        {
            _symbols.Add(symbol);
        }

        public int Count => _symbols.Count;

        public Symbol this[int index]
        {
            get
            {
                return _symbols[index];
            }
        }
    }
}
