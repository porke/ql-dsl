namespace QL.Core.Validation.Errors
{
    internal class BinaryOperatorType : Error
    {
        private string _leftType;
        private string _rightType;
        private string _opperator;

        public BinaryOperatorType(string leftType, string rightType, string opperator, int errorLine)
        {
            _leftType = leftType;
            _rightType = rightType;
            _opperator = opperator;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Type error in line {ErrorLine}: \'{_opperator}\' cannot be applied to " +
                $"the combination of {_leftType} and {_rightType}";
        }
    }
}
