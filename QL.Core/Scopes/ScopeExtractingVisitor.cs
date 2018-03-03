using QL.Core.Ast;
using QL.Core.Symbols;
using QL.Core.Types;
using System.Collections.Generic;

namespace QL.Core.Scopes
{
    public class ScopeExtractingVisitor : BaseVisitor<Scope>
    {
        private Stack<Scope> _scopes = new Stack<Scope>();
        private SymbolTable _symbolTable;
        private int _currentDepth;

        public ScopeExtractingVisitor(SymbolTable symbolTable)
        {
            _symbolTable = symbolTable;
        }
        
        public Scope GetTopLevelScope()
        {
            var scope = _scopes.Peek();
            while (scope.Parent != null)
            {
                scope = scope.Parent;
            }
            return scope;
        }

        public override Scope Visit(BlockNode node)
        {
            ++_currentDepth;

            if (_currentDepth > node.Depth)
            {
                for (int i = 0; i < _currentDepth - node.Depth; ++i)
                {
                    _scopes.Pop();
                }

                CreateChildScope();
                _currentDepth = node.Depth;
            }
            else if (_currentDepth == node.Depth)
            {
                var childScope = CreateChildScope();
                _scopes.Push(childScope);
            }            

            return _scopes.Peek();
        }

        public override Scope Visit(QuestionNode node)
        {
            _scopes.Peek().AddVariable(_symbolTable[node.Label]);
            return _scopes.Peek();
        }

        public override Scope Visit(VariableNode node)
        {
            _scopes.Peek().AddReference(_symbolTable[node.Label] ?? new Symbol(node.Label, QLType.Undefined, node.Token));
            return _scopes.Peek();
        }

        private Scope CreateChildScope()
        {
            Scope parentScope = _scopes.Count > 0 ? _scopes.Peek() : null;
            var newScope = new Scope(parentScope);
            parentScope?.Children.Add(newScope);
            return newScope;
        }
    }
}
