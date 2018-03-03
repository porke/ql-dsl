using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Api;
using QL.Core.Interpreting;

namespace QL.Core.Test.Interpreting
{
    [TestClass]
    public class InterpreterTests
    {
        private readonly IParserService _parsingService;

        public InterpreterTests()
        {
            _parsingService = ServiceRegistry.ParsingService;
        }

        [TestMethod]
        public void OneLiteralAssignment_WillChangeTheMemorySpace()
        {            
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("oneLiteralAssignment.ql"));
            var interpreter = new InterpreterVisitor();
            var newAst = interpreter.EvaluateAst(parsedSymbols.FormNode);

            Assert.Fail();   
        }
    }
}
