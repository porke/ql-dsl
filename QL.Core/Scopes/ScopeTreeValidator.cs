using QL.Core.Symbols; 
using System.Collections.Generic;

namespace QL.Core.Scopes
{
    class ScopeTreeValidator
    {
        public List<Symbol> UnreferencedVariables = new List<Symbol>();
        public List<Symbol> VariablesReferencedOutOfScope = new List<Symbol>();

        private bool FindReference(Scope scope, string referenceName)
        {
            if (scope.ContainsDefenition(referenceName))
            {
                return true;
            }

            return scope.Parent != null
                ? FindReference(scope.Parent, referenceName)
                : false;
        }

        public void CheckReferencesScope(Scope scope)
        {
            foreach (Symbol reference in scope.References)
            {
                if (reference.Type == SymbolType.Undefined)
                {
                    UnreferencedVariables.Add(reference);
                }
                else if (!FindReference(scope, reference.Name))
                {
                    VariablesReferencedOutOfScope.Add(reference);
                }
            }
            foreach (Scope child in scope.Childeren)
            {
                CheckReferencesScope(child);
            }
        }
    }
}
