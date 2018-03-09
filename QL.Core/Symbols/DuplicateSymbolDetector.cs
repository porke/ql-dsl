using QL.Api.Entities;
using QL.Core.Errors;
using System.Collections.Generic;

namespace QL.Core.Symbols
{
    internal class DuplicateSymbolDetector
    {
        internal IReadOnlyList<Error> FindDuplicateSymbols(SymbolTable symbolTable)
        {
            var errors = new List<Error>();
            var symbolNames = new HashSet<string>();
            foreach (var symbol in symbolTable)
            {
                if (symbolNames.Contains(symbol.Name))
                {
                    errors.Add(new VariableDuplicate(symbol.Name, symbol.Token?.Line ?? 0));
                }

                symbolNames.Add(symbol.Name);
            }

            return errors;
        }
    }
}
