using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Api;

namespace QL.Core.Test
{
    [TestClass]
    public class ConditionalParseTests
    {
        private readonly IParsingService _parsingService;
        private readonly AssertVisitor _assertVisitor;

        public ConditionalParseTests()
        {
            _parsingService = ServiceRegistry.ParsingService;
            _assertVisitor = new AssertVisitor();
    }

        [TestMethod]
        public void ParseIfConditionalOnly_WillSucceed()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("ifStatement.ql"));

            _assertVisitor.EnqueueConditionalNodeCallback(_ => { });
            _assertVisitor.EnqueueLiteralNodeCallback(literal =>
            {
                Assert.AreEqual("true", literal.Value);
            });

            parsedSymbols.FormNode.Accept(_assertVisitor);
            _assertVisitor.VerifyAll();
        }

        [TestMethod]
        public void ParseIfElseConditional_WillSucceed()
        {
            var parsedSymbols = _parsingService.ParseQLInput(TestDataResolver.LoadTestFile("ifElseStatement.ql"));

            _assertVisitor.EnqueueConditionalNodeCallback(_ => { });
            _assertVisitor.EnqueueConditionalNodeCallback(_ => { });
            _assertVisitor.EnqueueLiteralNodeCallback(literal =>
            {
                Assert.AreEqual("true", literal.Value);
            });
            _assertVisitor.EnqueueLiteralNodeCallback(literal =>
            {
                Assert.AreEqual("false", literal.Value);
            });

            parsedSymbols.FormNode.Accept(_assertVisitor);
            _assertVisitor.VerifyAll();
        }
    }
}
