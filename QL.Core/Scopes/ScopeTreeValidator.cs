using QL.Core.Errors;
using QL.Core.Symbols;
using QL.Core.Types;
using System.Collections.Generic;
using System.Linq;

namespace QL.Core.Scopes
{
    internal class ScopeTreeValidator
    {
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

        public List<Error> CheckReferencesScope(Scope scope)
        {
            var ScopeErrors = new List<Error>();
            foreach (Symbol reference in scope.References)
            {
                if (reference.Type == QLType.Undefined)
                {
                    ScopeErrors.Add(new VariableUndeclared(reference.Name, reference.Token?.Line ?? 0));
                }
                else if (!FindReference(scope, reference.Name))
                {
                    ScopeErrors.Add(new VariableDeclaredOutOfScope(reference.Name, reference.Token?.Line ?? 0));
                }
            }

            foreach (Scope child in scope.Children)
            {
                ScopeErrors.Concat(CheckReferencesScope(child));
            }

            return ScopeErrors;
        }
    }
}
