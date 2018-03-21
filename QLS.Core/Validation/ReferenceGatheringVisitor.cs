using QLS.Api.Ast;
using System.Collections.Generic;

namespace QLS.Core.Validation
{
    internal class ReferenceGatheringVisitor : BaseVisitor<Node>
    {
        public List<QuestionNode> Questions = new List<QuestionNode>();

        public override Node Visit(QuestionNode node)
        {
            Questions.Add(node);
            return VisitChildren(node);
        }
    }
}
