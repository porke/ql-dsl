namespace QL.Core.Validation.Errors
{
    internal class UnaryOperatorType : Error
    {
        private string _type;
        private string _opperator;

        public UnaryOperatorType(string type, string opperator, int errorLine)
        {
            _type = type;
            _opperator = opperator;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Type error in line {ErrorLine}: \'{_opperator}\' cannot be applied to {_type}";
        }
    }
}
