namespace QL.Core.Errors
{
    public class VariableUndeclared : Error
    {
        public string VariableName;
        public int ErrorLine;

        public VariableUndeclared(string variableName, int errorLine)
        {
            VariableName = variableName;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"The variable \"{VariableName}\" in line {ErrorLine} in undeclared.";
        }
    }
}
