using System.Collections.Generic;

namespace QL.Core.AST
{
    public class Statements : Node
    {
        public Statements(IList<Statement> statements)
        {
            _statements = statements;
        }        

        public Statement this[int key]
        {
            get
            {
                return _statements[key];
            }
        }

        private IList<Statement> _statements { get; }
    }
}
