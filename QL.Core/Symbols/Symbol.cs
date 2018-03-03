using Antlr4.Runtime;
using QL.Core.Types;

namespace QL.Core.Symbols
{
    public class Symbol
    {
        public Symbol(string name, QLType type, IToken token)
        {
            Name = name;
            Type = type;
            Token = token;
        }

        public string Name { get; }
        public QLType Type { get; }
        public IToken Token { get; }
    }
}
