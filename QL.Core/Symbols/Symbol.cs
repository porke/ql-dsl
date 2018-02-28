namespace QL.Core.Symbols
{
    public class Symbol
    {
        public Symbol(string name, SymbolType type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public SymbolType Type { get; }
    }
}
