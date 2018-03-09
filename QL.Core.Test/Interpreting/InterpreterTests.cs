using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api;
using QL.Api.Entities;
using QL.Api.Types;
using QL.Core.Interpreting;

namespace QL.Core.Test.Interpreting
{
    [TestClass]
    public class InterpreterTests
    {
        private readonly IParserService _parsingService;

        private AssertVisitor _assertVisitor;
        private InterpreterVisitor _interpreter;
        private MemorySystem _memory;

        public InterpreterTests()
        {
            _parsingService = Module.ParsingService;            
        }

        [TestInitialize]
        public void Setup()
        {
            _assertVisitor = new AssertVisitor();
            _interpreter = new InterpreterVisitor();
            _memory = new MemorySystem();
        }

        [TestMethod]
        public void OneLiteralAssignment_WillEvaluate()
        {            
            // Arrange
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("oneLiteralAssignment.ql"));
            var newAst = _interpreter.EvaluateAst(parsedSymbols.FormNode, _memory, parsedSymbols.Symbols);
            _assertVisitor.EnqueueLiteralNodeCallback(ln =>
            {
                Assert.AreEqual("100", ln.Value);
            });

            // Act
            newAst.Accept(_assertVisitor);

            // Verify
            _assertVisitor.VerifyAll();
        }

        [TestMethod]
        public void OneLiteralBooleanEvaluation_WillEvaluateCorrectly()
        {
            // Arrange
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("oneBooleanEvaluation.ql"));
            var newAst = _interpreter.EvaluateAst(parsedSymbols.FormNode, _memory, parsedSymbols.Symbols);
            _assertVisitor.EnqueueLiteralNodeCallback(ln =>
            {
                Assert.AreEqual(true, bool.Parse(ln.Value));
            });

            // Act
            newAst.Accept(_assertVisitor);

            // Verify
            _assertVisitor.VerifyAll();
        }

        [TestMethod]
        public void OneConditionalAssignment_WillRemoveTheIfBlock()
        {
            // Arrange
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("oneConditional.ql"));
            var newAst = _interpreter.EvaluateAst(parsedSymbols.FormNode, _memory, parsedSymbols.Symbols);
            _assertVisitor.EnqueueQuestionNodeCallback(q =>
            {
                Assert.AreEqual("elseQuestion", q.Description);
            });

            // Act
            newAst.Accept(_assertVisitor);

            // Verify
            _assertVisitor.VerifyAll();
        }

        [TestMethod]
        public void TwoQuestionsOneReference_WillResolveTheReferenceAndCalculateCorrectValue()
        {
            // Arrange
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("twoQuestionsOneReference.ql"));
            _memory.AssignValue("whatIsMeaning", new Value(42, QLType.Integer));
            var newAst = _interpreter.EvaluateAst(parsedSymbols.FormNode, _memory, parsedSymbols.Symbols);
            _assertVisitor.EnqueueLiteralNodeCallback(lt => 
            {
                Assert.AreEqual(42, int.Parse(lt.Value));
            });
            _assertVisitor.EnqueueLiteralNodeCallback(lt =>
            {
                Assert.AreEqual(84, int.Parse(lt.Value));
            });

            // Act
            newAst.Accept(_assertVisitor);

            // Verify
            _assertVisitor.VerifyAll();
        }
    }
}
