using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api;

namespace QL.Core.Test.Parsing
{
    [TestClass]
    public sealed class ExpressionParseTests
    {
        private readonly IParserService _parsingService;

        public ExpressionParseTests()
        {
            _parsingService = Module.ParsingService;
        }

        [TestMethod]
        public void ParseBinaryExpressionWithMissingOperand_WillReportError()
        {
            // Arrange & Act
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("binaryExpressionWithMissingOperand.ql"));

            // Assert
            Assert.AreEqual("Syntax error in line 9, character 0: extraneous input 'noItIsnt' expecting {'if', '}', STRING}.",
                parsedSymbols.Errors[0]);
        }
    }
}
