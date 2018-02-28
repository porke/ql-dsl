using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Ast;
using System;
using System.Collections.Generic;

namespace QL.Core.Test
{
    public sealed class AssertVisitor : BaseVisitor
    {
        private Queue<Action<FormNode>> _formNodeExpectations = new Queue<Action<FormNode>>();
        private Queue<Action<QuestionNode>> _questionNodeExpectations = new Queue<Action<QuestionNode>>();
        private Queue<Action<ConditionalNode>> _conditionalNodeExpectations = new Queue<Action<ConditionalNode>>();
        private Queue<Action<ExpressionNode>> _expressionNodeExpectations = new Queue<Action<ExpressionNode>>();
        private Queue<Action<VariableNode>> _variableNodeExpectations = new Queue<Action<VariableNode>>();
        private Queue<Action<LiteralNode>> _literalNodeExpectations = new Queue<Action<LiteralNode>>();

        public void EnqueueFormNodeCallback(Action<FormNode> assertionAction)
        {
            _formNodeExpectations.Enqueue(assertionAction);
        }

        public void EnqueueQuestionNodeCallback(Action<QuestionNode> assertionAction)
        {
            _questionNodeExpectations.Enqueue(assertionAction);
        }

        public void EnqueueConditionalNodeCallback(Action<ConditionalNode> assertionAction)
        {
            _conditionalNodeExpectations.Enqueue(assertionAction);
        }

        public void EnqueueExpressionNodeCallback(Action<ExpressionNode> assertionAction)
        {
            _expressionNodeExpectations.Enqueue(assertionAction);
        }

        public void EnqueueVariableNodeCallback(Action<VariableNode> assertionAction)
        {
            _variableNodeExpectations.Enqueue(assertionAction);
        }

        public void EnqueueLiteralNodeCallback(Action<LiteralNode> assertionAction)
        {
            _literalNodeExpectations.Enqueue(assertionAction);
        }

        public void VerifyAll()
        {
            Assert.AreEqual(0, _formNodeExpectations.Count, "Unmet expectation for a form node.");
            Assert.AreEqual(0, _questionNodeExpectations.Count, "Unmet expectation for a question node.");
            Assert.AreEqual(0, _conditionalNodeExpectations.Count, "Unmet expectation for a conditional node.");
            Assert.AreEqual(0, _expressionNodeExpectations.Count, "Unmet expectation for an expression node.");
            Assert.AreEqual(0, _variableNodeExpectations.Count, "Unmet expectation for a variable node.");
            Assert.AreEqual(0, _literalNodeExpectations.Count, "Unmet expectation for a literal node."); ;
        }

        override public void VisitEnter(BlockNode node)
        {
            // No action required
        }

        override public void VisitEnter(FormNode node)
        {
            if (_formNodeExpectations.Count > 0)
            {
                _formNodeExpectations.Dequeue().Invoke(node);
            }
        }

        override public void VisitEnter(QuestionNode node)
        {
            if (_questionNodeExpectations.Count > 0)
            {
                _questionNodeExpectations.Dequeue().Invoke(node);
            }
        }

        override public void VisitEnter(ConditionalNode node)
        {
            if (_conditionalNodeExpectations.Count > 0)
            {
                _conditionalNodeExpectations.Dequeue().Invoke(node);
            }
        }

        override public void VisitEnter(ExpressionNode node)
        {
            if (_expressionNodeExpectations.Count > 0)
            {
                _expressionNodeExpectations.Dequeue().Invoke(node);
            }
        }

        override public void VisitEnter(VariableNode node)
        {
            if (_variableNodeExpectations.Count > 0)
            {
                _variableNodeExpectations.Dequeue().Invoke(node);
            }
        }

        override public void VisitEnter(LiteralNode node)
        {
            if (_literalNodeExpectations.Count > 0)
            {
                _literalNodeExpectations.Dequeue().Invoke(node);
            }
        }
    }
}
