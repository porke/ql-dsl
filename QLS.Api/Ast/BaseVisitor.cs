using System.Linq;

namespace QLS.Api.Ast
{
    public abstract class BaseVisitor<T> : IVisitor<T>
    {
        protected virtual T VisitChildren(Node node) { return node.ChildNodes.Select(x => x.Accept(this)).LastOrDefault(); }

        public virtual T Visit(StylesheetNode node) { return VisitChildren(node); }
        public virtual T Visit(PageNode node) { return VisitChildren(node); }
        public virtual T Visit(SectionNode node) { return VisitChildren(node); }
        public virtual T Visit(QuestionNode node) { return VisitChildren(node); }
        public virtual T Visit(WidgetNode node) { return VisitChildren(node); }
    }
}
