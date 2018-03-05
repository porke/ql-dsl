using Antlr4.Runtime;
using QL.Core.Ast;
using QL.Core.Types;
using System.Linq;

namespace QL.Core.Interpreting
{
    internal class InterpreterVisitor : BaseVisitor<Node>
    {
        private MemorySystem _memory;

        internal Node EvaluateAst(Node ast, MemorySystem memory)
        {
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
                _memory.AssignValue(question.Label, new Value(evaluatedNode.Value));
                questionNode.AddChild(evaluatedNode);
            }

            if (!questionNode.ChildNodes.Any())
            {
                questionNode.ChildNodes.Add(new LiteralNode(question.Token, _memory.RetrieveValue(question.Label).ToString(), question.Type));
            }

            return questionNode;
        }

        public override Node Visit(ConditionalNode conditional)
        {
            Node expressionNode = conditional.ChildNodes[0];
            var evaluatedNodeValue = new Value((expressionNode.Accept(this) as LiteralNode).Value);
            if (evaluatedNodeValue.ToBool())
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
            // TODO: The interpreter currently can only correctly resolve variables which have already been defined
            // If the variable has not vet been evaluated then it will be initialized with a default value
            // This can lead to the final computation being incorrect.
            return new LiteralNode(variable.Token, _memory.RetrieveValue(variable.Label).ToString(), QLType.Undefined);
        }

        private Node EvaluateBinaryExpression(Node leftOperandNode, Node rightOperandNode, string @operator, IToken token)
        {
            // TODO: we need to introduce a proper type system and type promotion
            object leftOperandValue = (leftOperandNode.Accept(this) as LiteralNode).Value;
            object rightOperandValue = (rightOperandNode.Accept(this) as LiteralNode).Value;
            var leftValue = new Value(leftOperandValue);
            var rightValue = new Value(rightOperandValue);
            
            // TODO: the switch will need to be replaced with a different way of dispatching the correct logic
            switch (@operator)
            {
                case "-":
                    return new LiteralNode(token, (leftValue.ToInt() - rightValue.ToInt()).ToString(), QLType.Undefined);
                case "+":
                    return new LiteralNode(token, (leftValue.ToInt() + rightValue.ToInt()).ToString(), QLType.Undefined);
                case "*":
                    return new LiteralNode(token, (leftValue.ToInt() * rightValue.ToInt()).ToString(), QLType.Undefined);
                case "/":
                    return new LiteralNode(token, (leftValue.ToInt() / rightValue.ToInt()).ToString(), QLType.Undefined);
                case "||":
                    return new LiteralNode(token, (leftValue.ToBool() || rightValue.ToBool()).ToString(), QLType.Boolean);
                case "&&":
                    return new LiteralNode(token, (leftValue.ToBool() && rightValue.ToBool()).ToString(), QLType.Boolean);
                case ">":
                    return new LiteralNode(token, (leftValue.ToInt() > rightValue.ToInt()).ToString(), QLType.Boolean);
                case "<":
                    return new LiteralNode(token, (leftValue.ToInt() < rightValue.ToInt()).ToString(), QLType.Boolean);
                case ">=":
                    return new LiteralNode(token, (leftValue.ToInt() >= rightValue.ToInt()).ToString(), QLType.Boolean);
                case "<=":
                    return new LiteralNode(token, (leftValue.ToInt() <= rightValue.ToInt()).ToString(), QLType.Boolean);
                case "!=":
                    return new LiteralNode(token, leftValue.ToString().Equals(rightValue.ToString()).ToString(), QLType.Boolean);
                case "==":
                    return new LiteralNode(token, leftValue.ToString().Equals(rightValue.ToString()).ToString(), QLType.Boolean);
            }

            return null;
        }

        private Node EvaluateUnaryExpression(Node node, string @operator, IToken token)
        {
            object value = (node.Accept(this) as LiteralNode).Value;
            var nodeValue = new Value(value);
            switch (@operator)
            {
                case "-":
                    return new LiteralNode(token, (-nodeValue.ToInt()).ToString(), QLType.Undefined);
                case "!":
                    return new LiteralNode(token, (!nodeValue.ToBool()).ToString(), QLType.Boolean);
            }

            return null;
        }
    }
}
