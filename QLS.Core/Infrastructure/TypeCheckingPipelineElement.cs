using Infrastructure;
using QLS.Api.Infrastructure;
using QLS.Core.Validation;

namespace QLS.Core.Infrastructure
{
    internal class TypeCheckingPipelineElement : IPipelineElement<StylesheetTask>
    {
        private bool _canContinue = true;
        public bool CanContinue => _canContinue;

        public StylesheetTask Process(StylesheetTask input)
        {
            var TypeErrorExtractor = new TypeCheckingVisitor(input.QLSymbolTable);
            input.Ast.Accept(TypeErrorExtractor);

            TypeErrorExtractor.TypeErrors.ForEach(x => input.Errors.Add(x.ToString()));

            _canContinue = (input.Errors.Count == 0);

            return input;
        }
    }
}
