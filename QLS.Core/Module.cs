using Infrastructure;
using QLS.Api.Infrastructure;
using QLS.Core.Infrastructure;

namespace QLS.Core
{
    public static class Module
    {
        public static Pipeline<StylesheetTask> ParsingPipeline { get; } = CreateParsingPipeline();

        private static Pipeline<StylesheetTask> CreateParsingPipeline()
        {
            var pipeline = new Pipeline<StylesheetTask>();
            pipeline.ConnectElement(new ParsingPipelineElement());
            pipeline.ConnectElement(new DuplicationCheckingPipelineElement());
            pipeline.ConnectElement(new ReferenceCheckingPipelineElement());
            pipeline.ConnectElement(new DefinitionCheckingPipelineElement());

            return pipeline;
        }
    }
}
