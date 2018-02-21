using QL.Core.Ast;
using System.Collections.Generic;

namespace QL.Core.Api
{
    public class ParsedSymbols
    {
        public ParsedSymbols(Node formNode, IReadOnlyList<string> errors)
        {
            FormNode = formNode;
            Errors = errors;
        }

        public Node FormNode { get; }
        public IReadOnlyList<string> Errors {get;}
    }
}
