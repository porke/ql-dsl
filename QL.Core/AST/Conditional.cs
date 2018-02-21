namespace QL.Core.AST
{
    public class Conditional : Statement
    {
        public Conditional(Expression expression, Block ifBlock, Block elseBlock)
        {
            Expression = expression;
            IfBlock = ifBlock;
            ElseBlock = elseBlock;
        }

        public Expression Expression { get; }
        public Block IfBlock { get; }
        public Block ElseBlock { get; }
    }
}
