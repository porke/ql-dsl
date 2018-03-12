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

        [TestMethod]
        public void ParseSimpleStylesheetWithTwoPagesAndSections_WillSucceed()
        {
            Node ast = _parsingService.ParseQLSSheet(TestDataResolver.LoadTestFile("emptyStylesheetWithTwoPagesAndSections.qls"));

            _assertVisitor.EnqueueStylesheetNodeCallback(st =>
            {
                Assert.AreEqual("test", st.Label);
            });
            _assertVisitor.EnqueuePageNodeCallback(p =>
            {
                Assert.AreEqual("testPage", p.Label);
            });
            _assertVisitor.EnqueuePageNodeCallback(p =>
            {
                Assert.AreEqual("nextPage", p.Label);
            });
            _assertVisitor.EnqueueSectionNodeCallback(s =>
            {
                Assert.AreEqual("testSection", s.Label);
            });
            _assertVisitor.EnqueueSectionNodeCallback(s =>
            {
                Assert.AreEqual("nextSection", s.Label);
            });
            ast.Accept(_assertVisitor);
            _assertVisitor.VerifyAll();
        }

        [TestMethod]
        public void ParseSimpleStylesheetWithOnePageOneSectionOneQuestion_WillSucceed()
        {
            Node ast = _parsingService.ParseQLSSheet(TestDataResolver.LoadTestFile("onePageStylesheetWithQuestion.qls"));

            _assertVisitor.EnqueueStylesheetNodeCallback(st =>
            {
                Assert.AreEqual("test", st.Label);
            });
            _assertVisitor.EnqueuePageNodeCallback(p =>
            {
                Assert.AreEqual("testPage", p.Label);
            });
            _assertVisitor.EnqueueQuestionNodeCallback(q =>
            {
                Assert.AreEqual("questionOne", q.Label);
            });
            ast.Accept(_assertVisitor);
            _assertVisitor.VerifyAll();
        }        
    }
}
