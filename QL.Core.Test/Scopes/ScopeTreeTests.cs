using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Api;
using QL.Core.Scopes;
using QL.Core.Symbols;
using QL.Core.Types;

namespace QL.Core.Test.Scopes
{
    [TestClass]
    public class ScopeTreeTests
    {
        private readonly IParserService _parsingService;

        public ScopeTreeTests()
        {
            _parsingService = ServiceRegistry.ParsingService;
        }

        [TestMethod]
        public void ThreeNestedQuestions_ThreeScopesDetectedCorrectly()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("threeNestedQuestions.ql"));
            var symbolExtractor = new SymbolExtractingVisitor();
            parsedSymbols.FormNode.Accept(symbolExtractor);

            var scopeExtractor = new ScopeExtractingVisitor(symbolExtractor.SymbolTable);
            parsedSymbols.FormNode.Accept(scopeExtractor);

            Scope scope = scopeExtractor.GetTopLevelScope();
            Assert.AreEqual(1, scope.Variables.Count);
            Assert.AreEqual("whatIsMeaning", scope.Variables[0].Name);
            Assert.AreEqual(QLType.Money, scope.Variables[0].Type);
            Assert.AreEqual(1, scope.References.Count);
            Assert.AreEqual("whatIsMeaning", scope.References[0].Name);
            Assert.AreEqual(QLType.Money, scope.References[0].Type);
            Assert.AreEqual(1, scope.Children.Count);
            scope = scope.Children[0];
            Assert.AreEqual(1, scope.Variables.Count);
            Assert.AreEqual("hasSoldHouse", scope.Variables[0].Name);
            Assert.AreEqual(QLType.Boolean, scope.Variables[0].Type);
            Assert.AreEqual(1, scope.References.Count);
            Assert.AreEqual("whatIsMeaning", scope.References[0].Name);
            Assert.AreEqual(QLType.Money, scope.References[0].Type);
            Assert.AreEqual(1, scope.Children.Count);
            scope = scope.Children[0];
            Assert.AreEqual(1, scope.Variables.Count);
            Assert.AreEqual("dayToday", scope.Variables[0].Name);
            Assert.AreEqual(QLType.Date, scope.Variables[0].Type);
            Assert.AreEqual(0, scope.References.Count);
            Assert.AreEqual(0, scope.Children.Count);
        }
    }
}
