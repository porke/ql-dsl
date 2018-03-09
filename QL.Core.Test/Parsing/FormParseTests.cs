using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api.Infrastructure;
using QL.Core.Infrastructure;

namespace QL.Core.Test.Parsing
{
    [TestClass]
    public sealed class FormParseTests
    {
        private readonly Pipeline<ParsingTask> _parsingPipeline;
        private readonly AssertVisitor _assertVisitor;

        public FormParseTests()
        {
            _parsingPipeline = new Pipeline<ParsingTask>();
            _parsingPipeline.ConnectElement(new ParsingPipelineElement());

            _assertVisitor = new AssertVisitor();
        }

        [TestMethod]
        public void ParseEmptyFormWithNoStatements_WillSucceed()
        {
            // Arrange & Act
            var task = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("emptyForm.ql")));

            // Assert            
            _assertVisitor.EnqueueFormNodeCallback(formNode =>
            {
                Assert.AreEqual("empty", formNode.Label);
            });
            task.Ast.Accept(_assertVisitor);
        }

        [TestMethod]
        public void ParseEmptyFormWithSyntaxError_WillReportSyntaxError()
        {
            // Arrange & Act            
            var task = _parsingPipeline.Process(new ParsingTask("form { test }"));

            Assert.AreEqual("Syntax error in line 1, character 5: extraneous input '{' expecting LABEL.", task.Errors[0]);
        }

        [TestMethod]
        public void ParseEmptyString_WillReturnEmptyFormNode()
        {
            // Arrange & Act
            var task = _parsingPipeline.Process(new ParsingTask(""));

            Assert.AreEqual("Input string is empty", task.Errors[0]);
        }
    }
}
