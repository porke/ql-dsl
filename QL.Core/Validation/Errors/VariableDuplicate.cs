namespace QL.Core.Validation.Errors
{
    internal class VariableDuplicate : Error
    {
        private string _variableName;

        public VariableDuplicate(string variableName, int errorLine)
        {
            _variableName = variableName;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"The variable \"{_variableName}\" in line {ErrorLine} is already declared.";
        }
    }
}
