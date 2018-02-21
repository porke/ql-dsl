using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Api;

namespace QL.Core.Test
{
    [TestClass]
    public class QuestionParseTests
    {
        private readonly IParsingService _parsingService;
        private readonly AssertVisitor _assertVisitor;

        public QuestionParseTests()
        {
            _parsingService = ServiceRegistry.ParsingService;
            _assertVisitor = new AssertVisitor();
        }

        [TestMethod]
        public void ParseOneQuestionWithALabelNoStatements_WillSucceed()
        {
            // Arrange & Act
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("singleQuestion.ql"));

            // Assert
            _assertVisitor.EnqueueQuestionNodeCallback(question =>
            {
                Assert.AreEqual("whatIsMeaning", question.Label);
                Assert.AreEqual("What is the meaning of life?", question.Description);
                Assert.AreEqual("money", question.Type);
            });
            parsedSymbols.FormNode.Accept(_assertVisitor);
            _assertVisitor.VerifyAll();
        }

        [TestMethod]
        public void ParseMultipleQuestionsWithALabelNoStatements_WillSucceed()
        {
            // Arrange & Act
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("multipleQuestions.ql"));

            // Assert
            _assertVisitor.EnqueueQuestionNodeCallback(question =>
            {
                Assert.AreEqual("whatIsMeaning", question.Label);
                Assert.AreEqual("What is the meaning of life?", question.Description);
                Assert.AreEqual("money", question.Type);
            });
            _assertVisitor.EnqueueQuestionNodeCallback(question =>
            {
                Assert.AreEqual("hasSoldHouse", question.Label);
                Assert.AreEqual("Did you sell a house in 2010?", question.Description);
                Assert.AreEqual("boolean", question.Type);
            });
            _assertVisitor.EnqueueQuestionNodeCallback(question =>
            {
                Assert.AreEqual("dayToday", question.Label);
                Assert.AreEqual("Which day is today?", question.Description);
                Assert.AreEqual("date", question.Type);
            });
            parsedSymbols.FormNode.Accept(_assertVisitor);
            _assertVisitor.VerifyAll();
        }

        [TestMethod]
        public void ParseMultipleQuestionsWithASimpleAssignment_WillSucceed()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("questionWithSimpleAssignment.ql"));

            //Assert.AreEqual(3, parsedSymbols.Questions.Count);
            // TODO: Fix the test
        }

        [TestMethod]
        public void ParseSimpleExpression_AStatementExists()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("simpleExpression.ql"));

            //Assert.AreEqual(1, parsedSymbols.Forms[0].Statements.StatementList.Count);
            // TODO: Fix the test
        }
    }
}
