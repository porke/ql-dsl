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
        }

        protected Node(IToken token)
        {
            _token = token;
        }

        protected abstract void VisitNode(IVisitor visitor);

        protected void VisitChildren(IVisitor visitor)
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
