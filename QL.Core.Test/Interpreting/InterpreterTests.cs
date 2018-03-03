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
        public void OneLiteralAssignment_WillEvaluate()
        {            
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("oneLiteralAssignment.ql"));

            var interpreter = new InterpreterVisitor();
            var newAst = interpreter.EvaluateAst(parsedSymbols.FormNode);
            _assertVisitor.EnqueueLiteralNodeCallback(ln =>
            {
                Assert.AreEqual("100", ln.Value);
            });
            newAst.Accept(_assertVisitor);

            _assertVisitor.VerifyAll();
        }

        [TestMethod]
        public void OneConditionalAssignment_WillRemoveTheIfBlock()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("oneConditional.ql"));

            var interpreter = new InterpreterVisitor();
            var newAst = interpreter.EvaluateAst(parsedSymbols.FormNode);
            _assertVisitor.EnqueueQuestionNodeCallback(q =>
            {
                Assert.AreEqual("elseQuestion", q.Description);
            });
            newAst.Accept(_assertVisitor);

            _assertVisitor.VerifyAll();
        }
    }
}
