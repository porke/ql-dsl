using QL.Core.Ast;
using QL.Core.Scopes;
using QL.Core.Symbols;
using System.Collections.Generic;

namespace QL.Core.Api
{
    public class ParsedSymbols
    {
        public ParsedSymbols(Node formNode, SymbolTable symbols, Scope scopeTree, IReadOnlyList<string> errors)
        {
            FormNode = formNode;
            Errors = errors;
            Symbols = symbols;
            ScopeTree = scopeTree;
        }

        public Node FormNode { get; }
        public SymbolTable Symbols { get; }
        public Scope ScopeTree { get; }
        public IReadOnlyList<string> Errors { get; }
    } 
}
