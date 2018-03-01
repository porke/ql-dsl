namespace QL.Core.Errors
{
    public class NonBooleanInConditional : Error
    {
        public string Type;
        public int ErrorLine;

        public NonBooleanInConditional(string type, int errorLine)
        {
            Type = type;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Type error in line {ErrorLine}: required type was boolean, but {Type} was given. " +
                $"The expression in a conditional statement should always evaluate to a boolean.";
        }
    }
}

