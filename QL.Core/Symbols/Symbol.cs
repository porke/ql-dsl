using Antlr4.Runtime;

namespace QL.Core.Symbols
{
    public class Symbol
    {
        public Symbol(string name, SymbolType type, IToken token)
        {
            Name = name;
            Type = type;
            Token = token;
        }

        public string Name { get; }
        public SymbolType Type { get; }
        public IToken Token { get; }
    }
}
