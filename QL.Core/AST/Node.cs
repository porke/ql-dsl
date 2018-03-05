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
            if (node == null)
            {
                return;
            }

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

        public IToken Token => _token;
        public IList<Node> ChildNodes => _childNodes;
    }
}
