using System.Collections.Generic;

namespace QL.Core.AST
{
    public class QLForm
    {
        public string Label { get; set; }
        public List<QLStatement> Statements { get; set; } = new List<QLStatement>();
    }
}
