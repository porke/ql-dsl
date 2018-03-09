namespace QL.Core.Errors
{
    internal class UnaryOperatorType : Error
    {
        public string Type;
        public string Opperator;
        public int ErrorLine;

        public UnaryOperatorType(string type, string opperator, int errorLine)
        {
            Type = type;
            Opperator = opperator;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Type error in line {ErrorLine}: \'{Opperator}\' cannot be applied to {Type}";
        }
    }
}
