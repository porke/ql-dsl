using System.Linq;
using QL.Api.Infrastructure;
using QL.Core.Validation;

namespace QL.Core.Infrastructure
{
    internal class TypeCheckingPipelineElement : IPipelineElement<ParsingTask>
    {
        private bool _canContinue = true;

        public bool CanContinue => _canContinue;

        public ParsingTask Process(ParsingTask input)
        {
            if (input.SymbolTable == null)
            {
                _canContinue = false;
                return input;
            }

            var typeErrorExtractor = new TypeCheckingVisitor(input.SymbolTable);
            input.Ast.Accept(typeErrorExtractor);
            typeErrorExtractor.TypeErrors.ForEach(x => input.Errors.Add(x.ToString()));
            _canContinue = !typeErrorExtractor.TypeErrors.Any();

            return input;
        }
    }
}
