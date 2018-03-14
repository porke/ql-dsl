using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api.Infrastructure;
using QL.Core.Validation;

namespace QL.Core.Test.Validation
{
    [TestClass]
    public class ReferenceCheckingTests
    {
        private readonly Pipeline<ParsingTask> _parsingPipeline;

        public ReferenceCheckingTests()
        {
            _parsingPipeline = Module.ParsingPipeline;
        }

        [TestMethod]
        public void forwardReferencing_ErrorDetectedCorrectly()
        {
            // Arrange
            var parsingTask = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("forwardReferencing.ql")));
            var ReferenceErrorExtractor = new ReferenceCheckingVisitor();

            // Act
            parsingTask.Ast.Accept(ReferenceErrorExtractor);

            // Assert
            Assert.AreEqual(1, ReferenceErrorExtractor.ReferencingErrors.Count);
        }
    }
}
