using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Api;

namespace QL.Core.Test
{
    [TestClass]
    public class QLQuestionParseTests
    {
        private readonly IQLParsingService _parsingService;

        public QLQuestionParseTests()
        {
            _parsingService = ServiceRegistry.ParsingService;
        }

        [TestMethod]
        public void ParseOneQuestionWithALabelNoStatements_WillSucceed()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("singleQuestion.ql"));

            Assert.AreEqual(1, parsedSymbols.Questions.Count);
            Assert.AreEqual("whatIsMeaning", parsedSymbols.Questions[0].Variable.Label);
            Assert.AreEqual("What is the meaning of life?", parsedSymbols.Questions[0].Description);
            Assert.AreEqual("money", parsedSymbols.Questions[0].Type);
        }

        [TestMethod]
        public void ParseMultipleQuestionsWithALabelNoStatements_WillSucceed()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("multipleQuestions.ql"));
            
            Assert.AreEqual(6, parsedSymbols.Questions.Count);
            Assert.AreEqual("whatIsMeaning", parsedSymbols.Questions[0].Variable.Label);
            Assert.AreEqual("What is the meaning of life?", parsedSymbols.Questions[0].Description);
            Assert.AreEqual("money", parsedSymbols.Questions[0].Type);
        }

        [TestMethod]
        public void ParseMultipleQuestionsWithASimpleAssignment_WillSucceed()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("questionWithSimpleAssignment.ql"));

            Assert.AreEqual(3, parsedSymbols.Questions.Count);
        }

        [TestMethod]
        public void ParseSimpleExpression_AStatementExists()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("simpleExpression.ql"));

            Assert.AreEqual(1, parsedSymbols.Forms[0].Statements.StatementList.Count);
        }
    }
}
