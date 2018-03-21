namespace QLS.Core.Validation.Errors
{
    class DefinitionNotFound : Error
    {
        private string _questionName;

        public DefinitionNotFound(string questionName, int errorLine)
        {
            _questionName = questionName;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Reference error in line {ErrorLine}: \"{_questionName}\" was not defined in QL.";
        }
    }
}
