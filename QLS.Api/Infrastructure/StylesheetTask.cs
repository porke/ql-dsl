using QL.Api.Entities;
using QLS.Api.Ast;
using System.Collections.Generic;

namespace QLS.Api.Infrastructure
{
    public sealed class StylesheetTask
    {
        public StylesheetTask(string stylesheetText, SymbolTable symbolTable)
        {
            StylesheetText = stylesheetText;
            QLSymbolTable = symbolTable;
        }

        public string StylesheetText { get; }
        public IReadOnlyList<string> QuestionLabels { get; }
        public SymbolTable QLSymbolTable { get; }
        public Node Ast { get; set; }
        public IList<string> Errors { get; } = new List<string>();
    }
}
