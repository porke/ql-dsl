namespace QL.Core.Errors
{
    public class VariableDeclaredOutOfScope : Error
    {
        public string VariableName;
        public int ErrorLine;

        public VariableDeclaredOutOfScope(string variableName, int errorLine)
        {
            VariableName = variableName;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"The variable \"{VariableName}\" in line {ErrorLine} is declared in a different scope.";
        }
    }
}
