namespace QL.Core.AST
{
    public class Form : Node
    {
        public string Label { get; set; }
        public Statements Statements { get; set; }
    }
}
