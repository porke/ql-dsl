using Antlr4.Runtime;
using System.Collections.Generic;
using System.Linq;

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

        public T Accept<T>(IVisitor<T> visitor)
        {
            return VisitNode(visitor);
        }

        protected Node(IToken token)
        {
            _token = token;
        }

        protected abstract T VisitNode<T>(IVisitor<T> visitor);

        protected void VisitChildren<T>(IVisitor<T> visitor)
        {
            foreach (Node child in _childNodes)
            {
                child.Accept(visitor);
            }
        }

        public IToken Token => _token;
        public IList<Node> ChildNodes => _childNodes;
    }
}
