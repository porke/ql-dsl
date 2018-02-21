namespace QL.Core.AST
{
    public class BinaryExpression : Expression
    {
        public string Operator { get; set; }
        public Expression LeftExpression { get; set; }
        public Expression RightExpression { get; set; }
    }
}
