using Antlr4.Runtime;
using System.Collections.Generic;

namespace QL.Core.Ast
{
    public abstract class Node
    {
        protected readonly IToken _token;
        protected readonly IList<Node> _childNodes = new List<Node>();        

        public void AddChild(Node node)
        {
            _childNodes.Add(node);
        }

        public void Accept(IVisitor visitor)
        {
            VisitNode(visitor);
            VisitChildren(visitor);
        }

        protected Node(IToken token)
        {
            _token = token;
        }

        protected abstract void VisitNode(IVisitor visitor);

        private void VisitChildren(IVisitor visitor)
        {
            foreach (Node child in _childNodes)
            {
                child.Accept(visitor);
            }
        }
    }
}
