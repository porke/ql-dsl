using QLS.Api.Infrastructure;
using QLS.Core.Parsing;

namespace QLS.Core
{
    public static class Module
    {
        public static Pipeline<StylesheetTask> ParsingPipeline { get; } = CreateParsingPipeline();

        private static Pipeline<StylesheetTask> CreateParsingPipeline()
        {
            var pipeline = new Pipeline<StylesheetTask>();
            pipeline.ConnectElement(new ParsingPipelineElement());

            return pipeline;
        }
    }
}
