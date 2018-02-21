using System.Collections.Generic;

namespace QL.Core.AST
{
    public class Conditional : Statement
    {
        public Expression Expression { get; set; }
        public IList<Statement> IfStatements { get; set; } = new List<Statement>();
        public IList<Statement> ElseStatements { get; set; } = new List<Statement>();
    }
}
