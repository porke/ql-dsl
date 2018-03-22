using QL.Api.Ast;
using QL.Api.Entities;
using QL.Core.Validation.Errors;
using System.Collections.Generic;

namespace QL.Core.Validation
{
    // TODO: try to seprate gathering and analysing of data.
    internal class ReferenceCheckingVisitor : BaseVisitor<Scope>
    {
        public List<Error> ReferencingErrors = new List<Error>();
        private Stack<Scope> _scopes = new Stack<Scope>();
        private int _currentDepth;

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
            return VisitChildren(node);
        }

        public override Scope Visit(QuestionNode node)
        {
            Scope childeren = VisitChildren(node);
            _scopes.Peek().AddVariable(node.Label);
            return childeren;
        }

        public override Scope Visit(VariableNode node)
        {
            if (!checkReference(_scopes.Peek(), node.Label))
            {
                ReferencingErrors.Add(new ReferenceNotFound(node.Label, node.Token.Line));
            }
            return _scopes.Peek();
        }

        private bool checkReference(Scope scope, string reference)
        {
            if (scope.ContainsDefenition(reference))
            {
                return true;
            }
            else if (scope.Parent != null)
            {
                return checkReference(scope.Parent, reference);
            }
            return false;
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
