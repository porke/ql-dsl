using System.Linq;
using Antlr4.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Parsing;
using QL.Core.Api;

namespace QL.Core.Test
{
    [TestClass]
    public sealed class QLFormParseTests
    {
        private readonly IQLParsingService _parsingService;

        public QLFormParseTests()
        {
            _parsingService = new QLParsingService();
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
