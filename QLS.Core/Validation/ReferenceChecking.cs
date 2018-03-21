using QL.Api.Entities;
using QLS.Api.Ast;
using QLS.Core.Validation.Errors;
using System.Collections.Generic;


namespace QLS.Core.Validation
{
    internal class ReferenceChecking
    {
        public List<Error> ReferencingErrors = new List<Error>();
        private SymbolTable _symbolTable;
        private List<QuestionNode> _questions;

        public ReferenceChecking(SymbolTable symbolTable, List<QuestionNode> questions)
        {
            _symbolTable = symbolTable;
            _questions = questions;
        }

        public void checkReferences()
        {
            var questionNames = new HashSet<string>();
            foreach (var question in _questions)
            {
                questionNames.Add(question.Label);
            }

            for (int i = 0; i < _symbolTable.Count; i++)
            {
                var QLQuestion = _symbolTable[i];
                if (!questionNames.Contains(QLQuestion.Name))
                {
                    ReferencingErrors.Add(new QuestionNotReferenced(QLQuestion.Name, QLQuestion.Token?.Line ?? 0));
                }
            }
        }
    }
}
