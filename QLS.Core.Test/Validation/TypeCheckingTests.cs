using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api.Entities;
using QLS.Api.Infrastructure;

namespace QLS.Core.Test.Validation
{
    [TestClass]
    public class TypeCheckingTests
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
        public void RadioWidgetWithIntegerQuestion_OneTypeErrorFound()
        {
            // Arrange
            var QLquestions = new SymbolTable();
            QLquestions.Add(new Symbol("questionOne", QLType.Integer, null));
            var stylesheetTask = new StylesheetTask(TestDataResolver.LoadTestFile("onePageStylesheetWithQuestionAndWidget.qls"), QLquestions);

            // Act
            _parsingService.Process(stylesheetTask);

            // Assert
            Assert.AreEqual(1, stylesheetTask.Errors.Count);
            Assert.AreEqual("Type error in line 5: The type of \"questionOne\" (Integer) is not compatible with the type of the widget (Radio).", stylesheetTask.Errors[0]);
        }
    }
}
