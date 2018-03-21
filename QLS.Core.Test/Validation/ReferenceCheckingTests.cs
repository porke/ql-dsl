using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api.Entities;
using QLS.Api.Infrastructure;

namespace QLS.Core.Test.Validation
{
    [TestClass]
    public class ReferenceCheckingTests
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
        public void StyleSheetWithMissingReference_OneErrorFound()
        {
            // Arrange
            var QLquestions = new SymbolTable();
            QLquestions.Add(new Symbol("a", QLType.Undefined, null));
            var stylesheetTask = new StylesheetTask(TestDataResolver.LoadTestFile("emptyStylesheetWithOnePage.qls"), QLquestions);

            // Act
            _parsingService.Process(stylesheetTask);

            // Assert
            Assert.AreEqual(1, stylesheetTask.Errors.Count);
            Assert.AreEqual("Reference error in line 0 of the QL file: No style was defined for \"a\" in QLS.", stylesheetTask.Errors[0]);
        }
    }
}
