using System;
using System.Collections.Generic;

namespace QL.Core.Parsing
{
    public sealed class ParsingFailureException : Exception
    {
        public IReadOnlyList<string> ParsingErrors { get; }

        public ParsingFailureException(IReadOnlyList<string> errors)
        {
            ParsingErrors = errors;
        }
    }
}
