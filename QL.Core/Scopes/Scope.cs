using QL.Core.Symbols;
using System.Collections.Generic;
using System.Linq;

namespace QL.Core.Scopes
{
    public class Scope
    {
        public readonly IList<Scope> Children = new List<Scope>();
        public readonly Scope Parent;
        public readonly IList<Symbol> Variables = new List<Symbol>();
        public readonly IList<Symbol> References = new List<Symbol>();

        public Scope(Scope parent)
        {
            Parent = parent;
        }

        public void AddVariable(Symbol variable)
        {
            Variables.Add(variable);
        }

        public void AddReference(Symbol reference)
        {
            References.Add(reference);
        }

        public bool ContainsDefenition(string name)
        {
            return Variables.Any(s => s.Name == name);
        }
    }
}
