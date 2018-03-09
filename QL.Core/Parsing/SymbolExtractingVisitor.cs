using QL.Api.Ast;
using QL.Api.Entities;

namespace QL.Core.Parsing
{
    internal class SymbolExtractingVisitor : BaseVisitor<Symbol>
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
