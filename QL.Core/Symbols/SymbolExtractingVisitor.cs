using QL.Core.Ast;
using System;
using System.Collections.Generic;

namespace QL.Core.Symbols
{
    public class SymbolExtractingVisitor : BaseVisitor
    {
        public List<Symbol> SymbolTable = new List<Symbol>();

        public override void Visit(QuestionNode node)
        {
            var type = (SymbolType)Enum.Parse(typeof(SymbolType), node.Type, ignoreCase: true);
            SymbolTable.Add(new Symbol(node.Label, type));
        }
    }
}
