using QL.Core.AST;
using System.Collections.Generic;
using static QL.Core.QLParser;

namespace QL.Core
{
    public class QLVisitor : QLBaseVisitor<object>
    {
        public IList<QLForm> Forms { get; } = new List<QLForm>();

        public override object VisitForm(FormContext form)
        {
            Forms.Add(new QLForm { Label = form.GetChild(1).ToString() });

            return string.Empty;
        }  
    }
}
