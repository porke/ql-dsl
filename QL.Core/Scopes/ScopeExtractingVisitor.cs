using QL.Core.Ast;
using QL.Core.Symbols;
using QL.Core.Types;

namespace QL.Core.Scopes
{
    public class ScopeExtractingVisitor : BaseVisitor
    {
        public Scope ScopeTree;

        private Scope _currentScope;
        private SymbolTable _symbolTable;

        public ScopeExtractingVisitor(SymbolTable symbolTable)
        {
            _symbolTable = symbolTable;
        }

        public override void VisitExit(FormNode node)
        {
            ScopeTree = _currentScope;
        }

        public override void VisitEnter(BlockNode node)
        {
            var newScope = new Scope(_currentScope);
            _currentScope?.Children.Add(newScope);
            _currentScope = newScope;
        }

        public override void VisitExit(BlockNode node)
        {
            _currentScope = _currentScope.Parent ?? _currentScope;
        }

        public override void VisitEnter(QuestionNode node)
        {
            _currentScope.AddVariable(_symbolTable[node.Label]);
        }

        public override void VisitEnter(VariableNode node)
        {
            _currentScope.AddReference(_symbolTable[node.Label] ?? new Symbol(node.Label, QLType.Undefined, node.Token));
        }
    }
}
