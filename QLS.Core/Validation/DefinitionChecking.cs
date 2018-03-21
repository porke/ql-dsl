using QL.Api.Entities;
using QLS.Api.Ast;
using QLS.Core.Validation.Errors;
using System.Collections.Generic;


namespace QLS.Core.Validation
{
    internal class DefinitionChecking
    {
        public List<Error> ReferencingErrors = new List<Error>();
        private SymbolTable _symbolTable;
        private List<QuestionNode> _questions;

        public DefinitionChecking(SymbolTable symbolTable, List<QuestionNode> questions)
        {
            _symbolTable = symbolTable;
            _questions = questions;
        }

        public void checkDefinitions()
        {
            var questionNames = new HashSet<string>();
            for (int i = 0; i < _symbolTable.Count; i++)
            {
                questionNames.Add(_symbolTable[i].Name);
            }

            foreach (var question in _questions)
            {
                if (!questionNames.Contains(question.Label))
                {
                    ReferencingErrors.Add(new DefinitionNotFound(question.Label, question.Token?.Line ?? 0));
                }
            }
        }
    }
}
