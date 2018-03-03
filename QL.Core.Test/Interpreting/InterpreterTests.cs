using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Api;
using QL.Core.Ast;
using QL.Core.Interpreting;

namespace QL.Core.Test.Interpreting
{
    [TestClass]
    public class InterpreterTests
    {
        private readonly IParserService _parsingService;
        private readonly AssertVisitor _assertVisitor;

        public InterpreterTests()
        {
            _parsingService = ServiceRegistry.ParsingService;
            _assertVisitor = new AssertVisitor();
        }

        [TestMethod]
        public void OneLiteralAssignment_WillChangeTheMemorySpace()
        {            
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("oneLiteralAssignment.ql"));

            var interpreter = new InterpreterVisitor();
            var newAst = interpreter.EvaluateAst(parsedSymbols.FormNode);
            _assertVisitor.EnqueueLiteralNodeCallback(ln =>
            {
                Assert.AreEqual("100", ln.Value);
            });
            newAst.Accept<Node>(_assertVisitor);

            _assertVisitor.VerifyAll();
        }
    }
}
