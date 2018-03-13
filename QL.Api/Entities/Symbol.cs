using Antlr4.Runtime;

namespace QL.Api.Entities
{
    public sealed class Symbol
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
