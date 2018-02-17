using QL.Core.AST;
using System.Collections.Generic;
using static QL.Core.QLParser;

namespace QL.Core.Parsing
{
    public class QLFormVisitor : QLBaseVisitor<string>
    {
        public IList<QLForm> Forms { get; } = new List<QLForm>();

        public override string VisitForm(FormContext form)
        {
            Forms.Add(new QLForm { Label = form.LABEL().GetText() });

            return string.Empty;
        }
    }
}
