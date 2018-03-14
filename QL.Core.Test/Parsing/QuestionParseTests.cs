using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api.Entities;
using QL.Api.Infrastructure;
using QL.Core.Infrastructure;

namespace QL.Core.Test.Parsing
{
    [TestClass]
    public class QuestionParseTests
    {
        private readonly Pipeline<ParsingTask> _parsingPipeline;
        private readonly AssertVisitor _assertVisitor;

        public QuestionParseTests()
        {
            _parsingPipeline = new Pipeline<ParsingTask>();
            _parsingPipeline.ConnectElement(new ParsingPipelineElement());
            _assertVisitor = new AssertVisitor();
        }

        [TestMethod]
        public void ParseOneQuestionWithALabelNoConditional_WillSucceed()
        {
            // Arrange & Act
            var task = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("singleQuestion.ql")));

            // Assert
            _assertVisitor.EnqueueQuestionNodeCallback(question =>
            {
                Assert.AreEqual("whatIsMeaning", question.Label);
                Assert.AreEqual("What is the meaning of life?", question.Description);
                Assert.AreEqual(QLType.Decimal, question.Type);
            });
            task.Ast.Accept(_assertVisitor);
            _assertVisitor.VerifyAll();
        }

        [TestMethod]
        public void ParseMultipleQuestionsWithALabelNoConditional_WillSucceed()
        {
            // Arrange & Act
            var task = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("multipleQuestions.ql")));

            // Assert
            _assertVisitor.EnqueueQuestionNodeCallback(question =>
            {
                Assert.AreEqual("whatIsMeaning", question.Label);
                Assert.AreEqual("What is the meaning of life?", question.Description);
                Assert.AreEqual(QLType.Decimal, question.Type);
            });
            _assertVisitor.EnqueueQuestionNodeCallback(question =>
            {
                Assert.AreEqual("hasSoldHouse", question.Label);
                Assert.AreEqual("Did you sell a house in 2010?", question.Description);
                Assert.AreEqual(QLType.Boolean, question.Type);
            });
            _assertVisitor.EnqueueQuestionNodeCallback(question =>
            {
                Assert.AreEqual("dayToday", question.Label);
                Assert.AreEqual("Which day is today?", question.Description);
                Assert.AreEqual(QLType.Date, question.Type);
            });
            task.Ast.Accept(_assertVisitor);
            _assertVisitor.VerifyAll();
        }

        [TestMethod]
        public void ParseMultipleQuestionsWithASimpleAssignment_WillSucceed()
        {
            // Arrange & Act
            var task = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("questionWithSimpleAssignment.ql")));

            // Assert
            _assertVisitor.EnqueueQuestionNodeCallback(question =>
            {
                Assert.AreEqual("sellingPrice", question.Label);
                Assert.AreEqual("What was the selling price?", question.Description);
                Assert.AreEqual(QLType.Decimal, question.Type);
            });
            _assertVisitor.EnqueueQuestionNodeCallback(question =>
            {
                Assert.AreEqual("valueHouse", question.Label);
                Assert.AreEqual("Value house:", question.Description);
                Assert.AreEqual(QLType.Decimal, question.Type);
            });
            _assertVisitor.EnqueueVariableNodeCallback(variable =>
            {
                Assert.AreEqual("sellingPrice", variable.Label);
            });
            task.Ast.Accept(_assertVisitor);
            _assertVisitor.VerifyAll();
        }

        [TestMethod]
        public void ParseQuestionWithSimpleExpression_WillSucceed()
        {
            // Arrange & Act
            var task = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("simpleExpression.ql")));

            // Assert
            _assertVisitor.EnqueueQuestionNodeCallback(question =>
            {
                Assert.AreEqual("howTo", question.Label);
                Assert.AreEqual("Testtetstes?", question.Description);
                Assert.AreEqual(QLType.Integer, question.Type);
            });
            _assertVisitor.EnqueueExpressionNodeCallback(expression =>
            {
                Assert.AreEqual("-",expression.Operator.ToString());
            });
            _assertVisitor.EnqueueExpressionNodeCallback(expression =>
            {
                Assert.AreEqual("+", expression.Operator.ToString());
            });
            _assertVisitor.EnqueueLiteralNodeCallback(literal =>
            {
                Assert.AreEqual("1", literal.Value);
            });
            _assertVisitor.EnqueueLiteralNodeCallback(literal =>
            {
                Assert.AreEqual("2", literal.Value);
            });
            _assertVisitor.EnqueueLiteralNodeCallback(literal =>
            {
                Assert.AreEqual("3", literal.Value);
            });
            task.Ast.Accept(_assertVisitor);
            _assertVisitor.VerifyAll();
        }

        [TestMethod]
        public void ParseQuestionWithIncorrectType_WillReportError()
        {
            // Arrange & Act
            var task = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("wrongType.ql")));

            // Assert
            Assert.AreEqual("Syntax error in line 3, character 10: mismatched input 'cadeautje' expecting {'boolean', 'integer', 'decimal', 'string', 'date'}.",
                task.Errors[0]);
        }

        [TestMethod]
        public void ParseQuestionWithMissingLabel_WillReportError()
        {
            // Arrange & Act
            var task = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("missingLabel.ql")));

            // Assert
            Assert.AreEqual("Syntax error in line 3, character 0: missing LABEL at ':'.",
                task.Errors[0]);
        }

        [TestMethod]
        public void ParseQuestionWithMissingText_WillReportError()
        {
            // Arrange & Act
            var task = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("missingQuestionText.ql")));

            // Assert
            Assert.AreEqual("Syntax error in line 2, character 0: mismatched input 'whatIsThis' expecting {'if', '}', STRING}.",
                task.Errors[0]);
        }

        [TestMethod]
        public void ParseQuestionWithEmptyExpression_WillReportError()
        {
            // Arrange & Act
            var task = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("emptyExpression.ql")));

            // Assert
            Assert.AreEqual("Syntax error in line 4, character 0: mismatched input '}' expecting {'!', '-', '(', BOOLEAN, INTEGER, DECIMAL, STRING, DATE, LABEL}.",
                task.Errors[0]);
        }
    }
}
