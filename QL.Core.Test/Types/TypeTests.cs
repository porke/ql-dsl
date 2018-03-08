using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Api;
using QL.Core.Scopes;
using QL.Core.Symbols;
using QL.Core.Types;

namespace QL.Core.Test.Types
{
    [TestClass]
    public class TypeTests
    {
        private readonly IParserService _parsingService;

        public TypeTests()
        {
            _parsingService = ServiceRegistry.ParsingService;
        }

        // TODO: Implement tests.
        [TestMethod]
        public void QuestionsAssignment_OneAssignmentError()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("simpleExpression.ql"));
            var symbolExtractor = new SymbolExtractingVisitor();
            parsedSymbols.FormNode.Accept(symbolExtractor);
        }
    }
}
