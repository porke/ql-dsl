using QLS.Api.Ast;
using QLS.Core.Validation.Errors;
using System.Collections.Generic;

namespace QLS.Core.Validation
{
    internal class TypeCheckingVisitor : BaseVisitor<Node>
    {
        public List<Error> TypeErrors = new List<Error>();
    }
}
