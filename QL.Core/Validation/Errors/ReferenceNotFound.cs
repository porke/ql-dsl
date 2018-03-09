namespace QL.Core.Validation.Errors
{
    internal class ReferenceNotFound : Error
    {
        public string VariableName;
        public int ErrorLine;

        public ReferenceNotFound(string variableName, int errorLine)
        {
            VariableName = variableName;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"The variable \"{VariableName}\" in line {ErrorLine} has not been declared. (hint: QL does not allow forward referencing)";
        }
    }
}
