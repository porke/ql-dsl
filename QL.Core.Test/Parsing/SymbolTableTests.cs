using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api.Entities;
using QL.Api.Infrastructure;
using QL.Core.Infrastructure;

namespace QL.Core.Test.Parsing
{
    [TestClass]
    public class SymbolTableTests
    {
        private readonly Pipeline<ParsingTask> _parsingPipeline;

        public SymbolTableTests()
        {
            _parsingPipeline = new Pipeline<ParsingTask>();
            _parsingPipeline.ConnectElement(new ParsingPipelineElement());
            _parsingPipeline.ConnectElement(new SymbolExtractionPipelineElement());
        }

        [TestMethod]
        public void FormWithOneQuestion_OneSymbolDetectedCorrectly()
        {
            var parsingTask = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("singleQuestion.ql")));
            Assert.AreEqual(1, parsingTask.SymbolTable.Count);
            Assert.AreEqual("whatIsMeaning", parsingTask.SymbolTable[0].Name);
            Assert.AreEqual(QLType.Decimal, parsingTask.SymbolTable[0].Type);
        }

        [TestMethod]
        public void FormWithThreeQuestion_OneSymbolDetectedCorrectly()
        {
            var parsingTask = _parsingPipeline.Process(new ParsingTask(TestDataResolver.LoadTestFile("multipleQuestions.ql")));
            Assert.AreEqual(3, parsingTask.SymbolTable.Count);
            Assert.AreEqual("whatIsMeaning", parsingTask.SymbolTable[0].Name);
            Assert.AreEqual("hasSoldHouse", parsingTask.SymbolTable[1].Name);
            Assert.AreEqual("dayToday", parsingTask.SymbolTable[2].Name);
            Assert.AreEqual(QLType.Decimal, parsingTask.SymbolTable[0].Type);
            Assert.AreEqual(QLType.Boolean, parsingTask.SymbolTable[1].Type);
            Assert.AreEqual(QLType.Date, parsingTask.SymbolTable[2].Type);
        }
    }
}
