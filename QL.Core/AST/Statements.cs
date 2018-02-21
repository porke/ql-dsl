using System.Collections.Generic;

namespace QL.Core.AST
{
    public class Statements : Node
    {
        public List<Statement> StatementList { get; set; } = new List<Statement>();
    }
}
