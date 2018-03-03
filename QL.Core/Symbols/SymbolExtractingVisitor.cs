using QL.Core.Ast;

namespace QL.Core.Symbols
{
    public class SymbolExtractingVisitor : BaseVisitor
    {
        public SymbolTable SymbolTable = new SymbolTable();

        public override void VisitEnter(QuestionNode node)
        {
            SymbolTable.Add(new Symbol(node.Label, node.Type, node.Token));
        }
    }
}
