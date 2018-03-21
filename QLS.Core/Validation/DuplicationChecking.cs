using QLS.Api.Ast;
using QLS.Core.Validation.Errors;
using System.Collections.Generic;


namespace QLS.Core.Validation
{
    internal class DuplicationChecking
    {
        public List<Error> ReferencingErrors = new List<Error>();
        private List<QuestionNode> _questions;

        public DuplicationChecking(List<QuestionNode> questions)
        {
            _questions = questions;
        }

        public void checkDuplication()
        {
            var questionNames = new HashSet<string>();
            foreach (var question in _questions)
            {
                if (questionNames.Contains(question.Label))
                {
                    ReferencingErrors.Add(new DuplicateReference(question.Label, question.Token?.Line ?? 0));
                }

                questionNames.Add(question.Label);
            }
        }
    }
}
