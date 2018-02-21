using QL.Core.AST;
using System.Collections.Generic;

namespace QL.Core.Api
{
    public class ParsedSymbols
    {
        public IList<QLForm> Forms { get; set; }
        public IList<QLQuestion> Questions { get; set; }
    }
}
