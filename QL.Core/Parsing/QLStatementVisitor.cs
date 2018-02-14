using Antlr4.Runtime.Misc;
using static QL.Core.QLParser;

namespace QL.Core.Parsing
{
    public class QLStatementVisitor : QLBaseVisitor<string>
    {
        public override string VisitStatements([NotNull] StatementsContext context)
        {
            return base.VisitStatements(context);
        }
    }
}
