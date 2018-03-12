using QL.Api.Ast;
using QL.Api.Entities;
using QL.Api.Types;
using QL.Core.Validation.Errors;
using System.Collections.Generic;
using System.Linq;

namespace QL.Core.Validation
{
    internal class TypeCheckingVisitor : BaseVisitor<QLType>
    {
        public List<Error> TypeErrors = new List<Error>();
        private SymbolTable _symbolTable;

        public TypeCheckingVisitor(SymbolTable symbolTable)
        {
            _symbolTable = symbolTable;
        }

        public override QLType Visit(BlockNode node)
        {
            VisitChildren(node);
            return QLType.Undefined;
        }

        public override QLType Visit(ConditionalNode node)
        {
            foreach (Node child in node.ChildNodes)
            {
                QLType childType = child.Accept(this);
                if (childType != QLType.Boolean && childType != QLType.Undefined)
                {
                    TypeErrors.Add(new ConditionalType(childType.ToString(), node.Token.Line));
                }
            }
            return QLType.Boolean;
        }

        public override QLType Visit(QuestionNode node)
        {
            if (node.ChildNodes.Any())
            {
                QLType assignmentType = node.ChildNodes[0].Accept(this);
                if (assignmentType != node.Type && assignmentType != QLType.Undefined)
                {
                    TypeErrors.Add(new QuestionAssignmentType(node.Type.ToString(), assignmentType.ToString(), node.Token.Line));
                }
            }
            return node.Type;
        }

        public override QLType Visit(ExpressionNode node)
        {
            if (node.IsBinary)
            {
                return checkBinaryExpression(node);
            }
            else
            {
                return checkUnaryExpression(node);
            }
        }

        private QLType checkBinaryExpression(ExpressionNode node)
        {
            QLType leftHandType = node.ChildNodes[0].Accept(this);
            QLType rightHandType = node.ChildNodes[1].Accept(this);
            if (leftHandType == QLType.Undefined || rightHandType == QLType.Undefined)
            {
                return QLType.Undefined;
            }
            if (node.Operator.AcceptTypes(leftHandType, rightHandType))
            {
                return node.Operator.ResultingType(leftHandType, rightHandType);
            }
            TypeErrors.Add(new BinaryOperatorType(leftHandType.ToString(), rightHandType.ToString(), node.Operator.AsString, node.Token.Line));
            return QLType.Undefined;
        }

        private QLType checkUnaryExpression(ExpressionNode node)
        {
            QLType valueType = node.ChildNodes[0].Accept(this);
            if (valueType == QLType.Undefined)
            {
                return QLType.Undefined;
            }
            if (node.Operator.AcceptTypes(valueType))
            {
                TypeErrors.Add(new UnaryOperatorType(valueType.ToString(), node.Operator.AsString, node.Token.Line));
                return QLType.Undefined;
            }
            return valueType;
        }

        public override QLType Visit(VariableNode node)
        {
            return _symbolTable[node.Label].Type;
        }

        public override QLType Visit(LiteralNode node)
        {
            return node.Type;
        }
    }
}