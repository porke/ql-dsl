using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api.Infrastructure;

namespace QL.Core.Test.Parsing
{
    [TestClass]
    public class ConditionalParseTests
    {
        private readonly Pipeline<ParsingTask> _parsingPipeline;
        private readonly AssertVisitor _assertVisitor;

        public ConditionalParseTests()
        {
            _parsingPipeline = Module.ParsingPipeline;
            _assertVisitor = new AssertVisitor();
        }

        [TestMethod]
        public void ParseIfConditionalOnly_WillSucceed()
        {
            var parsedTask = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("ifStatement.ql")));

            _assertVisitor.EnqueueConditionalNodeCallback(_ => { });
            _assertVisitor.EnqueueLiteralNodeCallback(literal =>
            {
                Assert.AreEqual("true", literal.Value);
            });

            parsedTask.Ast.Accept(_assertVisitor);
            _assertVisitor.VerifyAll();
        }

        [TestMethod]
        public void ParseIfElseConditional_WillSucceed()
        {
            var parsedTask = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("ifElseStatement.ql")));

            _assertVisitor.EnqueueConditionalNodeCallback(_ => { });
            _assertVisitor.EnqueueConditionalNodeCallback(_ => { });
            _assertVisitor.EnqueueLiteralNodeCallback(literal =>
            {
                Assert.AreEqual("true", literal.Value);
            });
            _assertVisitor.EnqueueLiteralNodeCallback(literal =>
            {
                Assert.AreEqual("false", literal.Value);
            });

            parsedTask.Ast.Accept(_assertVisitor);
            _assertVisitor.VerifyAll();
        }

        [TestMethod]
        public void ParseIfWithAnUnclosedBlock_WillReportError()
        {
            // Arrange & Act
            var parsedTask = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("unclosedBlock.ql")));

            // Assert
            Assert.AreEqual("Syntax error in line 8, character 0: extraneous input '<EOF>' expecting {'if', '}', STRING}.",
                parsedTask.Errors[0]);
        }
    }
}
