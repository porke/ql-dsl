using System.Collections.Generic;

namespace QL.Core.AST
{
    public class QLStatement
    {
        public QLQuestion Question { get; set; }
        public IList<QLStatement> Statements { get; set; } = new List<QLStatement>();
    }
}
