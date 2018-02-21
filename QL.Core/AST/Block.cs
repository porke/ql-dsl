namespace QL.Core.AST
{
    public class Block : Node
    {
        public Block(Statements statements)
        {
            Statements = statements;
        }

        public Statements Statements { get; }
    }
}
