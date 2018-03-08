using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Api;

namespace QL.Core.Test.Types
{
    [TestClass]
    public class TypeTests
    {
        private readonly IParserService _parsingService;

        public TypeTests()
        {
            _parsingService = ServiceRegistry.ParsingService;
        }

        [TestMethod]
        public void DecimalInConditional_OneConditionalErrorDetected()
        {
            ParsedSymbols parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("decimalInCoditional.ql"));
            Assert.AreEqual(1, parsedSymbols.Errors.Count);
            Assert.AreEqual("Type error in line 2: required type was boolean, but Decimal was given. " +
                "The expression in a conditional statement should always evaluate to a boolean.", parsedSymbols.Errors[0]);
        }

        [TestMethod]
        public void WrongTypeInAssignment_OneAssignmentErrorDetected()
        {
            ParsedSymbols parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("boolAssignedToInt.ql"));
            Assert.AreEqual(1, parsedSymbols.Errors.Count);
            Assert.AreEqual("Type error in line 2: The question is of type \'Integer\' "
                    + "but the assignment evaluates to a(n) Boolean", parsedSymbols.Errors[0]);
        }

        [TestMethod]
        public void WrongTypesInBinaryOperator_OneBinaryOperatorTypeErrorDetected()
        {
            ParsedSymbols parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("binaryExpressionWithWrongTypes.ql"));
            Assert.AreEqual(1, parsedSymbols.Errors.Count);
            Assert.AreEqual("Type error in line 3: \'?\' cannot be applied to " +
                "the combination of Decimal and Boolean", parsedSymbols.Errors[0]);
        }
    }
}
