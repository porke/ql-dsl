namespace QLS.Core.Validation.Errors
{
    class QuestionNotReferenced : Error
    {
        private string _questionName;

        public QuestionNotReferenced(string questionName, int errorLine)
        {
            _questionName = questionName;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Reference error in line {ErrorLine}: No style was defined for {_questionName} in QLS.";
        }
    }
}
