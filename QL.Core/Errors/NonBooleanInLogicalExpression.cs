namespace QL.Core.Errors
{
    public class NonBooleanInLogicalExpression : Error
    {
        public string Type;
        public int ErrorLine;

        public NonBooleanInLogicalExpression(string type, int errorLine)
        {
            Type = type;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Type error in line {ErrorLine}: required type was boolean, but {Type} was given. " +
                $"Logical opperators only accept booleans.";
        }
    }
}
