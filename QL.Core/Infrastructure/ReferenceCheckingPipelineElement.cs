using QL.Api.Infrastructure;
using QL.Core.Validation;

namespace QL.Core.Infrastructure
{
    internal class ReferenceCheckingPipelineElement : IPipelineElement<ParsingTask>
    {
        public bool CanContinue => true;
                
        public ParsingTask Process(ParsingTask input)
        {
            var referenceErrorExtractor = new ReferenceCheckingVisitor();
            input.Ast.Accept(referenceErrorExtractor);
            referenceErrorExtractor.ReferencingErrors.ForEach(x => input.Errors.Add(x.ToString()));

            return input;
        }
    }
}
