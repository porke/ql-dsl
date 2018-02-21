using System.Collections.Generic;

namespace QL.Core.AST
{
    public abstract class QLStatement
    {
        
    }

    public class QLQuestion : QLStatement
    {
        public string Description { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public QLExpression Expression { get; set; }
    }

    public class QLConditional : QLStatement
    {
        public QLExpression Expression { get; set; }
        public List<QLStatement> IfStatements { get; set; } = new List<QLStatement>();
        public List<QLStatement> ElseStatements { get; set; } = new List<QLStatement>();
    }
}
