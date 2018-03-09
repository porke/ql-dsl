using QL.Api.Ast;
using QL.Api.Entities;

namespace QL.Api.Infrastructure
{
    public sealed class InterpretingTask
    {
        public InterpretingTask(Node ast, MemorySystem memory, SymbolTable symbols)
        {
            InitialAst = ast;
            Memory = memory;
            Symbols = symbols;
        }

        public Node InitialAst { get; }
        public MemorySystem Memory { get; }
        public SymbolTable Symbols { get; }
        public Node InterpretedAst { get; set; }
    }
}
