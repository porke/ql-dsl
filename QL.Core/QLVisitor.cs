using static QL.Core.QLParser;

namespace QL.Core
{
    public class QLVisitor : QLBaseVisitor<object>
    {
        public override object VisitForm(FormContext form)
        {
            return form.children[1].GetText().StartsWith(" test");
        }
    }
}
