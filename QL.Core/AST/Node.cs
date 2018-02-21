using Antlr4.Runtime;
using System.Collections.Generic;

namespace QL.Core.AST
{
    public class Node
    {
        private readonly IToken _token;
        private readonly IList<Node> _childNodes = new List<Node>();

        public Node(IToken token)
        {
            _token = token;
        }

        public void AddChild(Node node)
        {
            _childNodes.Add(node);
        }

        //public string ToStringTree()
        //{
        //    if (children == null || children.size() == 0) return this.toString();
        //    var buf = new StringBuilder();
        //    if (!isNil())
        //    {
        //        buf.append("(");
        //        buf.append(this.toString());
        //        buf.append(' ');
        //    }
        //    for (int i = 0; i < children.size(); i++)
        //    {
        //        AST t = (AST)children.get(i); // normalized (unnamed) children
        //        if (i > 0) buf.append(' ');
        //        buf.append(t.toStringTree());
        //    }
        //    if (!isNil()) buf.append(")");
        //    return buf.toString();
        //}
    }
}
