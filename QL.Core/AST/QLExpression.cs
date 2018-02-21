namespace QL.Core.AST
{
    public abstract class QLExpression
    {
        
    }

    public class QLUnExpression : QLExpression
    {
        public QLOperator Operator { get; set; }
        public QLExpression Expression { get; set; }
    }

    public class QLBinExpression : QLExpression
    {
        public QLOperator Operator { get; set; }
        public QLExpression LeftExpression { get; set; }
        public QLExpression RightExpression { get; set; }
    }

    public class QLLitExpression : QLExpression
    {
        public string Value { get; set; }
    }

    public class QLVarExpression : QLExpression
    {
        public string Label { get; set; }
    }
}
