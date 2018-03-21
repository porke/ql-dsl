using Infrastructure;
using QLS.Api.Infrastructure;
using QLS.Core.Validation;

namespace QLS.Core.Infrastructure
{
    internal class DefinitionCheckingPipelineElement : IPipelineElement<StylesheetTask>
    {
        private bool _canContinue = true;
        public bool CanContinue => _canContinue;

        public StylesheetTask Process(StylesheetTask input)
        {
            var ReferenceExtractor = new ReferenceGatheringVisitor();
            input.Ast.Accept(ReferenceExtractor);

            var referenceErrorExtractor = new DefinitionChecking(input.QLSymbolTable, ReferenceExtractor.Questions);
            referenceErrorExtractor.checkDefinitions();
            referenceErrorExtractor.ReferencingErrors.ForEach(x => input.Errors.Add(x.ToString()));

            _canContinue = (input.Errors.Count == 0);

            return input;
        }
    }
}
