using QL.Core.Ast;

namespace QL.Core.Symbols
{
    public class SymbolExtractingVisitor : BaseVisitor<Symbol>
    {
        public SymbolTable SymbolTable = new SymbolTable();

        public override Symbol Visit(QuestionNode node)
        {
            var symbol = new Symbol(node.Label, node.Type, node.Token);
            SymbolTable.Add(new Symbol(node.Label, node.Type, node.Token));
            return symbol;
        }
    }
}
