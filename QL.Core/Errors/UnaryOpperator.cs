namespace QL.Core.Errors
{
    public class UnaryOpperator : Error
    {
        public string Type;
        public string Opperator;
        public int ErrorLine;

        public UnaryOpperator(string type, string opperator, int errorLine)
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
