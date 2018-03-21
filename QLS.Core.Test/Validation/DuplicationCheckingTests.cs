using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api.Entities;
using QLS.Api.Infrastructure;

namespace QLS.Core.Test.Validation
{
    [TestClass]
    public class DuplicationCheckingTests
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
        public void StyleSheetWithDuplicateReference_OneErrorFound()
        {
            // Arrange
            var stylesheetTask = new StylesheetTask(TestDataResolver.LoadTestFile("onePageStylesheetDuplicateQuestion.qls"), new SymbolTable());

            // Act
            _parsingService.Process(stylesheetTask);

            // Assert
            Assert.AreEqual(1, stylesheetTask.Errors.Count);
            Assert.AreEqual("Reference error in line 5: \"questionOne\" was already defined.", stylesheetTask.Errors[0]);
        }
    }
}
