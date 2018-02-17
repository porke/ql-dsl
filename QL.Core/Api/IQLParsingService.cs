using QL.Core.AST;
using System.Collections.Generic;

namespace QL.Core.Api
{
    public interface IQLParsingService
    {
        IList<QLQuestion> Questions { get; }

        void ParseQLInput(string input);
    }
}
