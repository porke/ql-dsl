using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Api;
using QL.Core.Scopes;

namespace QL.Core.Test.References
{
    [TestClass]
    public class ReferenceCheckingTests
    {
        private readonly IParserService _parsingService;

        public ReferenceCheckingTests()
        {
            _parsingService = ServiceRegistry.ParsingService;
        }

        [TestMethod]
        public void forwardReferencing_ErrorDetectedCorrectly()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("forwardReferencing.ql"));
            var ReferenceErrorExtractor = new ReferenceCheckingVisitor();
            parsedSymbols.FormNode.Accept(ReferenceErrorExtractor);

            Assert.AreEqual(1, ReferenceErrorExtractor.ReferencingErrors.Count);
        }
    }
}
