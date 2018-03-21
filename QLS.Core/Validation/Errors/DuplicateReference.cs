namespace QLS.Core.Validation.Errors
{
    class DuplicateReference : Error
    {
        private string _questionName;
        private int _orginalQuestionLine;

        public DuplicateReference(string questionName, int orginalQuestionLine, int errorLine)
        {
            _questionName = questionName;
            _orginalQuestionLine = orginalQuestionLine;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Reference error in line {ErrorLine}: {_questionName} was already defined in line {_orginalQuestionLine}.";
        }
    }
}
