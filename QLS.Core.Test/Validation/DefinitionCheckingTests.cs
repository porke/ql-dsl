using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api.Entities;
using QLS.Api.Infrastructure;

namespace QLS.Core.Test.Validation
{
    [TestClass]
    public class DefinitionCheckingTests
    {
        private AssertVisitor _assertVisitor;
        private Pipeline<StylesheetTask> _parsingService;

        [TestInitialize]
        public void Setup()
        {
            _assertVisitor = new AssertVisitor();
            _parsingService = Module.ParsingPipeline;
        }

        [TestMethod]
        public void StyleSheetWithMissingDefinition_OneErrorFound()
        {
            // Arrange
            var stylesheetTask = new StylesheetTask(TestDataResolver.LoadTestFile("onePageStylesheetWithQuestion.qls"), new SymbolTable());

            // Act
            _parsingService.Process(stylesheetTask);

            // Assert
            Assert.AreEqual(1, stylesheetTask.Errors.Count);
            Assert.AreEqual("Reference error in line 4: \"questionOne\" was not defined in QL.", stylesheetTask.Errors[0]);
        }
    }
}
