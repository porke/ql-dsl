namespace QLS.Core.Validation.Errors
{
    class ReferenceNotFound : Error
    {
        private string _questionName;

        public ReferenceNotFound(string questionName, int errorLine)
        {
            _questionName = questionName;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Reference error in line {ErrorLine}: {_questionName} was not referenced in QL.";
        }
    }
}
