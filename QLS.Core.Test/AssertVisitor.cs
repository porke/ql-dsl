using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLS.Api.Ast;
using System;
using System.Collections.Generic;

namespace QLS.Core.Test
{
    public sealed class AssertVisitor : BaseVisitor<Node>
    {
        private Queue<Action<StylesheetNode>> _stylesheetNodeExpectations = new Queue<Action<StylesheetNode>>();
        private Queue<Action<PageNode>> _pageNodeExpectations = new Queue<Action<PageNode>>();

        public void EnqueueStylesheetNodeCallback(Action<StylesheetNode> assertionAction)
        {
            _stylesheetNodeExpectations.Enqueue(assertionAction);
        }

        public void EnqueuePageNodeCallback(Action<PageNode> assertionAction)
        {
            _pageNodeExpectations.Enqueue(assertionAction);
        }

        public void VerifyAll()
        {
            Assert.AreEqual(0, _stylesheetNodeExpectations.Count, "Unmet expectations for a stylesheet node.");
            Assert.AreEqual(0, _pageNodeExpectations.Count, "Unmet expectations for a page node.");
        }

        override public Node Visit(StylesheetNode node)
        {
            if (_stylesheetNodeExpectations.Count > 0)
            {
                _stylesheetNodeExpectations.Dequeue().Invoke(node);
            }

            return VisitChildren(node);
        }

        override public Node Visit(PageNode node)
        {
            if (_pageNodeExpectations.Count > 0)
            {
                _pageNodeExpectations.Dequeue().Invoke(node);
            }

            return VisitChildren(node);
        }
    }
}
