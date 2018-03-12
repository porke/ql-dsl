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
        private Queue<Action<SectionNode>> _sectionNodeExpectations = new Queue<Action<SectionNode>>();
        private Queue<Action<QuestionNode>> _questionNodeExpectations = new Queue<Action<QuestionNode>>();

        public void EnqueueStylesheetNodeCallback(Action<StylesheetNode> assertionAction)
        {
            _stylesheetNodeExpectations.Enqueue(assertionAction);
        }

        public void EnqueuePageNodeCallback(Action<PageNode> assertionAction)
        {
            _pageNodeExpectations.Enqueue(assertionAction);
        }

        public void EnqueueSectionNodeCallback(Action<SectionNode> assertionAction)
        {
            _sectionNodeExpectations.Enqueue(assertionAction);
        }

        public void EnqueueQuestionNodeCallback(Action<QuestionNode> assertionAction)
        {
            _questionNodeExpectations.Enqueue(assertionAction);
        }

        public void VerifyAll()
        {
            Assert.AreEqual(0, _stylesheetNodeExpectations.Count, "Unmet expectations for stylesheet nodes.");
            Assert.AreEqual(0, _pageNodeExpectations.Count, "Unmet expectations for page nodes.");
            Assert.AreEqual(0, _sectionNodeExpectations.Count, "Unmet expectations for section nodes.");
            Assert.AreEqual(0, _questionNodeExpectations.Count, "Unmet expectations for question nodes.");
        }

        public override Node Visit(SectionNode node)
        {
            if (_sectionNodeExpectations.Count > 0)
            {
                _sectionNodeExpectations.Dequeue().Invoke(node);
            }

            return VisitChildren(node);
        }

        public override Node Visit(StylesheetNode node)
        {
            if (_stylesheetNodeExpectations.Count > 0)
            {
                _stylesheetNodeExpectations.Dequeue().Invoke(node);
            }

            return VisitChildren(node);
        }

        public override Node Visit(QuestionNode node)
        {
            if (_questionNodeExpectations.Count > 0)
            {
                _questionNodeExpectations.Dequeue().Invoke(node);
            }

            return VisitChildren(node);
        }

        public override Node Visit(PageNode node)
        {
            if (_pageNodeExpectations.Count > 0)
            {
                _pageNodeExpectations.Dequeue().Invoke(node);
            }

            return VisitChildren(node);
        }
    }
}
