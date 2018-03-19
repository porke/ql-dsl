namespace QL.Core.Validation.Errors
{
    internal class ConditionalType : Error
    {
        private string _type;

        public ConditionalType(string type, int errorLine)
        {
            _type = type;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Type error in line {ErrorLine}: required type was boolean, but {_type} was given. " +
                $"The expression in a conditional statement should always evaluate to a boolean.";
        }
    }
}

