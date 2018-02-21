using Antlr4.Runtime;
using QL.Core.Ast.Visitors;

namespace QL.Core.Ast
{
    public class ConditionalNode : Node
    {
        public ConditionalNode(IToken token) : base(token)
        {

        }

        protected override void VisitNode(IVisitor visitor)
        {
            // TODO: Implement soon
        }
    }
}
