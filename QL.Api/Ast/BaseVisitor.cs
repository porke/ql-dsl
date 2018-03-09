using System.Linq;

namespace QL.Api.Ast
{
    public abstract class BaseVisitor<T> : IVisitor<T>
    {        
        protected virtual T VisitChildren(Node node) { return node.ChildNodes.Select(x => x.Accept(this)).LastOrDefault();  }

        public virtual T Visit(FormNode node) { return VisitChildren(node); }
        public virtual T Visit(VariableNode node) { return VisitChildren(node); }
        public virtual T Visit(ConditionalNode node) { return VisitChildren(node); }
        public virtual T Visit(LiteralNode node) { return VisitChildren(node); }
        public virtual T Visit(ExpressionNode node) { return VisitChildren(node); }
        public virtual T Visit(QuestionNode node) { return VisitChildren(node); }
        public virtual T Visit(BlockNode node) { return VisitChildren(node); }        
    }
}
