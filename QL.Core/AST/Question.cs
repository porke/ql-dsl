namespace QL.Core.AST
{
    public class Question : Statement
    {
        public string Description { get; set; }
        public Variable Variable { get; set; }
        public string Type { get; set; }
        public Expression Expression { get; set; }
    }
}
