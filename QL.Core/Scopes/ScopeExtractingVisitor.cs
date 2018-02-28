using QL.Core.Ast;
using QL.Core.Symbols;
using System.Collections.Generic;

namespace QL.Core.Scopes
{
    public class ScopeExtractingVisitor : BaseVisitor
    {
        public Scope ScopeTree;
        private Scope CurrentScope;
        public SymbolTable SymbolTable;

        public ScopeExtractingVisitor(SymbolTable symbolTable)
        {
            SymbolTable = symbolTable;
        }

        public override void VisitExit(FormNode node)
        {
            ScopeTree = CurrentScope;
        }

        public override void VisitEnter(BlockNode node)
        {
            var newScope = new Scope(CurrentScope);
            CurrentScope?.Childeren.Add(newScope);
            CurrentScope = newScope;
        }

        public override void VisitExit(BlockNode node)
        {
            CurrentScope = CurrentScope.Parent ?? CurrentScope;
        }

        public override void VisitEnter(QuestionNode node)
        {
            CurrentScope.AddVariable(SymbolTable[node.Label]);
        }

        public override void VisitEnter(VariableNode node)
        {
            CurrentScope.AddReference(SymbolTable[node.Label] ?? new Symbol(node.Label, SymbolType.Undefined, node.Token));
        }
    }
}
