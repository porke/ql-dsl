using Infrastructure;
using QLS.Api.Infrastructure;
using QLS.Core.Validation;

namespace QLS.Core.Infrastructure
{
    internal class DuplicationCheckingPipelineElement : IPipelineElement<StylesheetTask>
    {
        private bool _canContinue = true;
        public bool CanContinue => _canContinue;

        public StylesheetTask Process(StylesheetTask input)
        {
            var ReferenceExtractor = new ReferenceGatheringVisitor();
            input.Ast.Accept(ReferenceExtractor);

            var duplicationErrorExtractor = new DuplicationChecking(ReferenceExtractor.Questions);
            duplicationErrorExtractor.checkDuplication();
            duplicationErrorExtractor.ReferencingErrors.ForEach(x => input.Errors.Add(x.ToString()));

            _canContinue = (input.Errors.Count == 0);

            return input;
        }
    }
}
