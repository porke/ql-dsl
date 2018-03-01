namespace QL.Core.Errors
{
    public class Comparison : Error
    {
        public string LeftType;
        public string RightType;
        public int ErrorLine;

        public Comparison(string leftType, string rightType, int errorLine)
        {
            LeftType = leftType;
            RightType = rightType;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Type error in line {ErrorLine}: {LeftType} cannot be compared to {RightType}";
        }
    }
}
