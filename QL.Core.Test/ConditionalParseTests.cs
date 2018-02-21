using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Api;

namespace QL.Core.Test
{
    [TestClass]
    public class ConditionalParseTests
    {
        private readonly IParsingService _parsingService;

        public ConditionalParseTests()
        {
            _parsingService = ServiceRegistry.ParsingService;
        }

        [TestMethod]
        public void ParseIfConditionalOnly_WillSucceed()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("ifStatement.ql"));
            //Assert.IsTrue(parsedSymbols.Form.Block.Statements[0] != null);
        }

        [TestMethod]
        public void ParseIfElseConditional_WillSucceed()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("ifElseStatement.ql"));            
        }
    }
}
