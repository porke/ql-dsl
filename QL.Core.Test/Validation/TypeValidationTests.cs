using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api.Infrastructure;

namespace QL.Core.Test.Validation
{
    [TestClass]
    public class TypeValidationTests
    {
        private readonly Pipeline<ParsingTask> _parsingPipeline;

        public TypeValidationTests()
        {
            _parsingPipeline = Module.ParsingPipeline;
        }

        [TestMethod]
        public void DecimalInConditional_OneConditionalErrorDetected()
        {
            // Arrange & Act
            var parsingTasks = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("decimalInCoditional.ql")));

            // Assert
            Assert.AreEqual(1, parsingTasks.Errors.Count);
            Assert.AreEqual("Type error in line 2: required type was boolean, but Decimal was given. " +
                "The expression in a conditional statement should always evaluate to a boolean.", parsingTasks.Errors[0]);
        }

        [TestMethod]
        public void WrongTypeInAssignment_OneAssignmentErrorDetected()
        {
            // Arrange & Act
            var parsingTasks = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("boolAssignedToInt.ql")));

            // Assert
            Assert.AreEqual(1, parsingTasks.Errors.Count);
            Assert.AreEqual("Type error in line 2: The question is of type \'Integer\' "
                    + "but the assignment evaluates to a(n) Boolean", parsingTasks.Errors[0]);
        }

        [TestMethod]
        public void WrongTypesInBinaryOperator_OneBinaryOperatorTypeErrorDetected()
        {
            // Arrange & Act
            var parsingTasks = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("binaryExpressionWithWrongTypes.ql")));

            // Assert
            Assert.AreEqual(1, parsingTasks.Errors.Count);
            Assert.AreEqual("Type error in line 3: \'+\' cannot be applied to " +
                "the combination of Decimal and Boolean", parsingTasks.Errors[0]);
        }
    }
}
