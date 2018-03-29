using QL.Api.Entities;
using QLS.Api.Ast;
using QLS.Core.Validation.Errors;
using System.Collections.Generic;

namespace QLS.Core.Validation
{
    internal class TypeCheckingVisitor : BaseVisitor<Node>
    {
        public List<Error> TypeErrors = new List<Error>();
        private Stack<QuestionNode> _questionTypes = new Stack<QuestionNode>();
        private SymbolTable _symbolTable;

        public TypeCheckingVisitor(SymbolTable symbolTable)
        {
            _symbolTable = symbolTable;
        }

        public override Node Visit(QuestionNode node)
        {
            _questionTypes.Push(node);
            var childeren = VisitChildren(node);
            _questionTypes.Pop();
            return childeren;
        }

        public override Node Visit(WidgetNode node)
        {
            var question = _questionTypes.Peek();
            var matchingQLSymbol = _symbolTable[question.Label];
            if (!node.Widget.IsCompatibleWith(matchingQLSymbol.Type))
            {
                TypeErrors.Add(new WidgetNotCompatible(question.Label, 
                    matchingQLSymbol.Type.ToString(), node.Widget.ToString(), node.Token.Line));
            }
            return VisitChildren(node);
        }
    }
}
