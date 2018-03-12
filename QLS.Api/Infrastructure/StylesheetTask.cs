using QLS.Api.Ast;
using System.Collections.Generic;

namespace QLS.Api.Infrastructure
{
    public sealed class StylesheetTask
    {
        public StylesheetTask(string stylesheetText, IReadOnlyList<string> questionLabels)
        {
            StylesheetText = stylesheetText;
            QuestionLabels = questionLabels;
        }

        public string StylesheetText { get; }
        public IReadOnlyList<string> QuestionLabels { get; }
        public Node Ast { get; set; }
        public IList<string> Errors { get; } = new List<string>();
    }
}
