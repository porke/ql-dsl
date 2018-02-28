using System.Collections.Generic;

namespace QL.Core.Symbols
{
    internal class DuplicateSymbolDetector
    {
        internal IReadOnlyList<Symbol> FindDuplicateSymbols(SymbolTable symbolTable)
        {
            var duplicateSymbols = new List<Symbol>();
            var symbolNames = new HashSet<string>();
            foreach (var symbol in symbolTable)
            {
                if (symbolNames.Contains(symbol.Name))
                {
                    duplicateSymbols.Add(symbol);
                }

                symbolNames.Add(symbol.Name);
            }

            return duplicateSymbols;
        }
    }
}
