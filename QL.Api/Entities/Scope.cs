using System.Collections.Generic;
using System.Linq;

namespace QL.Api.Entities
{
    public class Scope
    {
        public readonly IList<Scope> Children = new List<Scope>();
        public readonly Scope Parent;
        public readonly IList<string> Variables = new List<string>();

        public Scope(Scope parent)
        {
            Parent = parent;
        }

        public void AddVariable(string variable)
        {
            Variables.Add(variable);
        }

        public bool ContainsDefenition(string name)
        {
            return Variables.Any(s => s == name);
        }
    }
}
