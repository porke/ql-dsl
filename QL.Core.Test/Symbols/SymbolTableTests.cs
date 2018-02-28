using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Api;
using QL.Core.Symbols;

namespace QL.Core.Test.Symbols
{
    [TestClass]
    public class SymbolTableTests
    {
        private readonly IParsingService _parsingService;

        public SymbolTableTests()
        {
            _parsingService = ServiceRegistry.ParsingService;
        }

        [TestMethod]
        public void FormWithOneQuestion_OneSymbolDetectedCorrectly()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("singleQuestion.ql"));

            var symbolExtractor = new SymbolExtractingVisitor();
            parsedSymbols.FormNode.Accept(symbolExtractor);

            Assert.AreEqual(1, symbolExtractor.SymbolTable.Count);
            Assert.AreEqual("whatIsMeaning", symbolExtractor.SymbolTable[0].Name);
            Assert.AreEqual(SymbolType.Money, symbolExtractor.SymbolTable[0].Type);
        }

        [TestMethod]
        public void FormWithThreeQuestion_OneSymbolDetectedCorrectly()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("multipleQuestions.ql"));

            var symbolExtractor = new SymbolExtractingVisitor();
            parsedSymbols.FormNode.Accept(symbolExtractor);

            Assert.AreEqual(3, symbolExtractor.SymbolTable.Count);
            Assert.AreEqual("whatIsMeaning", symbolExtractor.SymbolTable[0].Name);
            Assert.AreEqual("hasSoldHouse", symbolExtractor.SymbolTable[1].Name);
            Assert.AreEqual("dayToday", symbolExtractor.SymbolTable[2].Name);
            Assert.AreEqual(SymbolType.Money, symbolExtractor.SymbolTable[0].Type);
            Assert.AreEqual(SymbolType.Boolean, symbolExtractor.SymbolTable[1].Type);
            Assert.AreEqual(SymbolType.Date, symbolExtractor.SymbolTable[2].Type);
        }
    }
}
