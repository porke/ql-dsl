using QL.Core.AST;
using System.Collections.Generic;

namespace QL.Core.Api
{
    public class ParsedSymbols
    {
        public IList<Form> Forms { get; set; }
        public IList<Question> Questions { get; set; }
    }
}
