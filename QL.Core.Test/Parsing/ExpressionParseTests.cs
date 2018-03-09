using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api.Infrastructure;

namespace QL.Core.Test.Parsing
{
    [TestClass]
    public sealed class ExpressionParseTests
    {
        [TestMethod]
        public void ParseBinaryExpressionWithMissingOperand_WillReportError()
        {
            // Arrange & Act
            var parsedSymbols = Module.ParsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("binaryExpressionWithMissingOperand.ql")));

            // Assert
            Assert.AreEqual("Syntax error in line 9, character 0: extraneous input 'noItIsnt' expecting {'if', '}', STRING}.",
                parsedSymbols.Errors[0]);
        }
    }
}
