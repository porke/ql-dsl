using Infrastructure;
using QL.Api.Infrastructure;
using QL.Core.Parsing;

namespace QL.Core.Infrastructure
{
    internal class SymbolExtractionPipelineElement : IPipelineElement<ParsingTask>
    {
        public bool CanContinue => true;

        public ParsingTask Process(ParsingTask input)
        {
            var symbolTableVisitor = new SymbolExtractingVisitor();
            input.Ast.Accept(symbolTableVisitor);
            input.SymbolTable = symbolTableVisitor.SymbolTable;
            return input;
        }
    }
}
