using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api.Entities;
using QL.Api.Factories;
using QL.Api.Infrastructure;
using QL.Core.Interpreting;
using static QL.Core.Interpreting.Value;

namespace QL.Core.Test.Interpreting
{
    [TestClass]
    public class InterpreterTests
    {
        private readonly Pipeline<ParsingTask> _parsingPipeline;

        private AssertVisitor _assertVisitor;
        private InterpreterVisitor _interpreter;
        private MemorySystem _memory;
        private IValueFactory _valueFactory;

        public InterpreterTests()
        {
            _parsingPipeline = Module.ParsingPipeline;            
        }

        [TestInitialize]
        public void Setup()
        {
            _assertVisitor = new AssertVisitor();
            _valueFactory = new ValueFactory();
            _interpreter = new InterpreterVisitor(_valueFactory);
            _memory = new MemorySystem();
        }

        [TestMethod]
        public void OneLiteralAssignment_WillEvaluate()
        {
            // Arrange
            var parsingTask = new ParsingTask(TestDataResolver.LoadTestFile("oneLiteralAssignment.ql"));
            ParsingTask taskOutput = _parsingPipeline.Process(parsingTask);
            var newAst = _interpreter.EvaluateAst(taskOutput.Ast, _memory, taskOutput.SymbolTable);
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
            var parsingTask = new ParsingTask(TestDataResolver.LoadTestFile("oneBooleanEvaluation.ql"));
            ParsingTask taskOutput = _parsingPipeline.Process(parsingTask);
            var newAst = _interpreter.EvaluateAst(taskOutput.Ast, _memory, taskOutput.SymbolTable);
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
            var parsingTask = new ParsingTask(TestDataResolver.LoadTestFile("oneConditional.ql"));
            ParsingTask taskOutput = _parsingPipeline.Process(parsingTask);
            var newAst = _interpreter.EvaluateAst(parsingTask.Ast, _memory, parsingTask.SymbolTable);
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
            var parsingTask = new ParsingTask(TestDataResolver.LoadTestFile("twoQuestionsOneReference.ql"));
            ParsingTask taskOutput = _parsingPipeline.Process(parsingTask);
            _memory.AssignValue("whatIsMeaning", _valueFactory.CreateValue(42, QLType.Integer));
            var newAst = _interpreter.EvaluateAst(parsingTask.Ast, _memory, parsingTask.SymbolTable);
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
