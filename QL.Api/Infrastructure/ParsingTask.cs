using QL.Api.Ast;
using QL.Api.Entities;
using System.Collections.Generic;

namespace QL.Api.Infrastructure
{
    public sealed class ParsingTask
    {
        public ParsingTask(string parsingInput)
        {
            ParsingInput = parsingInput;
        }

        public string ParsingInput { get; }
        public Node Ast { get; set; }
        public SymbolTable SymbolTable { get; set; }
        public MemorySystem Memory { get; set; }
        public Scope ScopeTree { get; set; }
        public IList<string> Errors { get; } = new List<string>();
    }
}
