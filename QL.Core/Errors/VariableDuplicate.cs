namespace QL.Core.Errors
{
    internal class VariableDuplicate : Error
    {
        public string VariableName;
        public int ErrorLine;

        public VariableDuplicate(string variableName, int errorLine)
        {
            VariableName = variableName;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"The variable \"{VariableName}\" in line {ErrorLine} is already declared.";
        }
    }
}
