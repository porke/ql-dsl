using Antlr4.Runtime;
using System.Collections.Generic;

namespace QL.Core.Parsing
{
    internal class ErrorListener : BaseErrorListener
    {
        public List<string> Errors { get; } = new List<string>();

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            base.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e);
            Errors.Add($"Syntax error in line {line}, character {charPositionInLine}: {msg}.");
        }
    }
}
