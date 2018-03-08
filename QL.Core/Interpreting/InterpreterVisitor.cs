﻿using Antlr4.Runtime;
using QL.Core.Ast;
using QL.Core.Operators;
using QL.Core.Symbols;
using QL.Core.Types;
using System.Linq;

namespace QL.Core.Interpreting
{
    internal class InterpreterVisitor : BaseVisitor<Node>
    {
        private SymbolTable _symbols;
        private MemorySystem _memory;

        internal Node EvaluateAst(Node ast, MemorySystem memory, SymbolTable symbols)
        {
            _symbols = symbols;
            _memory = memory;
            return ast.Accept(this);
        }

        public override Node Visit(FormNode form)
        {
            var newForm = new FormNode(form.Token, form.Label);
            foreach (var block in form.ChildNodes)
            {
                newForm.AddChild(block.Accept(this));
            }
            return newForm;
        }

        public override Node Visit(BlockNode block)
        {
            var blockNode = new BlockNode(block.Token, block.Depth);
            foreach (var statement in block.ChildNodes)
            {
                blockNode.AddChild(statement.Accept(this));
            }
            return blockNode;
        }

        public override Node Visit(QuestionNode question)
        {
            var questionNode = new QuestionNode(question.Token, question.Description, question.Label, question.IsEvaluated, question.Type);
            foreach (var expression in question.ChildNodes)
            {
                var evaluatedNode = expression.Accept(this) as LiteralNode;
                _memory.AssignValue(question.Label, new Value(evaluatedNode.Value, question.Type));
                questionNode.AddChild(evaluatedNode);
            }

            if (!questionNode.ChildNodes.Any())
            {
                Value memoryValue;
                if (!_memory.TryRetrieveValue(question.Label, out memoryValue))
                {
                    memoryValue = new Value(question.Type);
                }

                questionNode.ChildNodes.Add(new LiteralNode(question.Token, memoryValue.ToString(), question.Type));
            }

            return questionNode;
        }

        public override Node Visit(ConditionalNode conditional)
        {
            Node expressionNode = conditional.ChildNodes[0];
            var evaluatedNodeValue = new Value((expressionNode.Accept(this) as LiteralNode).Value, QLType.Boolean);
            if (evaluatedNodeValue.ToBoolean())
            {
                var ifNode = conditional.ChildNodes[1];
                return ifNode.Accept(this);
            }
            else if (conditional.ChildNodes.Count > 2)
            {
                var elseNode = conditional.ChildNodes[2];
                return elseNode.Accept(this);
            }

            return null;
        }

        public override Node Visit(ExpressionNode expression)
        {
            if (expression.IsBinary)
            {
                return EvaluateBinaryExpression(expression.ChildNodes[0], expression.ChildNodes[1], expression.Operator, expression.Token);
            }
            else if (expression.IsUnary)
            {
                return EvaluateUnaryExpression(expression.ChildNodes[0], expression.Operator, expression.Token);
            }

            return expression.Accept(this);
        }

        public override Node Visit(LiteralNode literal)
        {
            return new LiteralNode(literal.Token, literal.Value, literal.Type);
        }

        public override Node Visit(VariableNode variable)
        {
            Value memoryValue;
            if (!_memory.TryRetrieveValue(variable.Label, out memoryValue))
            {
                memoryValue = new Value(_symbols[variable.Label].Type);
            }
            return new LiteralNode(variable.Token, memoryValue.ToString(), memoryValue.Type);
        }

        private Node EvaluateBinaryExpression(Node leftOperandNode, Node rightOperandNode, Operator @operator, IToken token)
        {
            var leftOperandLiteral = leftOperandNode.Accept(this) as LiteralNode;
            var rightOperandLiteral = rightOperandNode.Accept(this) as LiteralNode;
            var leftValue = new Value(leftOperandLiteral.Value, leftOperandLiteral.Type);
            var rightValue = new Value(rightOperandLiteral.Value, rightOperandLiteral.Type);

            return new LiteralNode(token, @operator.Evaluate(leftValue, rightValue).ToString(), QLType.Undefined); 
        }

        private Node EvaluateUnaryExpression(Node node, Operator @operator, IToken token)
        {
            var operand = node.Accept(this) as LiteralNode;
            var nodeValue = new Value(operand.Value, operand.Type);

            return new LiteralNode(token, @operator.Evaluate(nodeValue).ToString(), QLType.Undefined);
        }
    }
}
