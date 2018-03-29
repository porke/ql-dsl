namespace QLS.Core.Validation.Errors
{
    internal class QuestionNotReferenced : Error
    {
        private string _questionName;

        public QuestionNotReferenced(string questionName, int errorLine)
        {
            _questionName = questionName;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Reference error in line {ErrorLine} of the QL file: No style was defined for \"{_questionName}\" in QLS.";
        }
    }
}
