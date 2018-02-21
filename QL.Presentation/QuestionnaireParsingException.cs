using System;
using System.Collections.Generic;

namespace QL.Presentation
{
    internal class QuestionnaireParsingException : Exception
    {
        internal IReadOnlyList<string> Errors { get; }

        internal QuestionnaireParsingException(IReadOnlyList<string> errors)
        {
            Errors = errors;
        }
    }
}
