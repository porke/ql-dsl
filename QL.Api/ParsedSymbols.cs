using QL.Api.Ast;
using QL.Api.Entities;
using System.Collections.Generic;

namespace QL.Api
{
    public class ParsedSymbols
    {
        public ParsedSymbols(Node formNode, SymbolTable symbols, IReadOnlyList<string> errors)
        {
            FormNode = formNode;
            Errors = errors;
            Symbols = symbols;
        }

        public Node FormNode { get; }
        public SymbolTable Symbols { get; }
        public IReadOnlyList<string> Errors { get; }
    } 
}
