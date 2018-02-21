using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Api;

namespace QL.Core.Test
{
    [TestClass]
    public sealed class FormParseTests
    {
        private readonly IParsingService _parsingService;

        public FormParseTests()
        {
            _parsingService = ServiceRegistry.ParsingService;
        }

        [TestMethod]
        public void ParseEmptyFormWithNoStatements_WillSucceed()
        {
            // Arrange & Act
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("emptyForm.ql"));

            // Assert
            var formVisitor = new AssertVisitor();
            formVisitor.EnqueueFormAssert(formNode =>
            {
                Assert.AreEqual("empty", formNode.Label);
            });
            parsedSymbols.Form.Accept(formVisitor);            
        }
    }
}
