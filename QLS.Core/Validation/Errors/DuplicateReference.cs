namespace QLS.Core.Validation.Errors
{
    internal class DuplicateReference : Error
    {
        private string _questionName;

        public DuplicateReference(string questionName, int errorLine)
        {
            _questionName = questionName;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Reference error in line {ErrorLine}: \"{_questionName}\" was already defined.";
        }
    }
}
