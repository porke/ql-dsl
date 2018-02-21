using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Api;

namespace QL.Core.Test
{
    [TestClass]
    public sealed class QLFormParseTests
    {
        private readonly IQLParsingService _parsingService;

        public QLFormParseTests()
        {
            _parsingService = ServiceRegistry.ParsingService;
        }

        [TestMethod]
        public void ParseEmptyFormWithNoStatements_WillSucceed()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("emptyForm.ql"));
            
            Assert.AreEqual(1, parsedSymbols.Forms.Count);
            Assert.AreEqual("empty", parsedSymbols.Forms[0].Label);
            Assert.IsFalse(parsedSymbols.Forms[0].Statements.Any());
        }
    }
}
