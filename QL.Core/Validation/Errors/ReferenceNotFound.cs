namespace QL.Core.Validation.Errors
{
    internal class ReferenceNotFound : Error
    {
        private string _variableName;

        public ReferenceNotFound(string variableName, int errorLine)
        {
            _variableName = variableName;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"The variable \"{_variableName}\" in line {ErrorLine} has not been declared. (hint: QL does not allow forward referencing)";
        }
    }
}
