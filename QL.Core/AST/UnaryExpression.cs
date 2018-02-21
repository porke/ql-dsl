namespace QL.Core.AST
{
    public class UnaryExpression : Expression
    {
        public string Operator { get; set; }
        public Expression Expression { get; set; }
    }
}
