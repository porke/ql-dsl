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
            formVisitor.EnqueueFormNodeCallback(formNode =>
            {
                Assert.AreEqual("empty", formNode.Label);
            });
            parsedSymbols.FormNode.Accept(formVisitor);            
        }

        [TestMethod]
        public void ParseEmptyFormWithSyntaxError_WillReportSyntaxError()
        {
            // Arrange & Act
            var parsedSymbols = _parsingService.ParseQLInput("form { test }");

            Assert.AreEqual("Syntax error in line 1, character 5: extraneous input '{' expecting LABEL.", parsedSymbols.Errors[0]);
        }
    }
}
