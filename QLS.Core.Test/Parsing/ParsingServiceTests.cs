using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLS.Api.Ast;

namespace QLS.Core.Test.Parsing
{
    [TestClass]
    public class ParsingServiceTests
    {
        private AssertVisitor _assertVisitor;
        private IParsingService _parsingService;

        [TestInitialize]
        public void Setup()
        {
            _assertVisitor = new AssertVisitor();
            _parsingService = Module.ParsingService;
        }

        [TestMethod]
        public void ParseSimpleStylesheetWithOnePage_WillSucceed()
        {
            Node ast = _parsingService.ParseQLSSheet(TestDataResolver.LoadTestFile("emptyStylesheetWithOnePage.qls"));

            _assertVisitor.EnqueueStylesheetNodeCallback(st =>
            {
                Assert.AreEqual("test", st.Label);
            });
            _assertVisitor.EnqueuePageNodeCallback(p =>
            {
                Assert.AreEqual("testPage", p.Label);
            });
            ast.Accept(_assertVisitor);
            _assertVisitor.VerifyAll();
        }
    }
}
