using QL.Core.Ast;
using System;

namespace QL.Core.Symbols
{
    public class SymbolExtractingVisitor : BaseVisitor
    {
        public SymbolTable SymbolTable = new SymbolTable();

        public override void VisitEnter(QuestionNode node)
        {
            var type = (SymbolType)Enum.Parse(typeof(SymbolType), node.Type, ignoreCase: true);
            SymbolTable.Add(new Symbol(node.Label, type, node.Token));
        }
    }
}
