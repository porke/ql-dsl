using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLS.Api.Ast;
using QLS.Api.Entities;
using QLS.Api.Infrastructure;
using QLS.Core.Parsing;
using System.Collections.Generic;

namespace QLS.Core.Test.Parsing
{
    [TestClass]
    public class ParsingServiceTests
    {
        private AssertVisitor _assertVisitor;
        private ParsingPipelineElement _parsingService;

        [TestInitialize]
        public void Setup()
        {
            _assertVisitor = new AssertVisitor();
            _parsingService = new ParsingPipelineElement();
        }

        [TestMethod]
        public void ParseSimpleStylesheetWithOnePage_WillSucceed()
        {
            var stylesheetTask = new StylesheetTask(TestDataResolver.LoadTestFile("emptyStylesheetWithOnePage.qls"), new List<string>());
            Node ast = _parsingService.Process(stylesheetTask).Ast;

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
            var stylesheetTask = new StylesheetTask(TestDataResolver.LoadTestFile("emptyStylesheetWithTwoPagesAndSections.qls"), new List<string>());
            Node ast = _parsingService.Process(stylesheetTask).Ast;

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
            var stylesheetTask = new StylesheetTask(TestDataResolver.LoadTestFile("onePageStylesheetWithQuestion.qls"), new List<string>());
            Node ast = _parsingService.Process(stylesheetTask).Ast;

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

        [TestMethod]
        public void ParseSimpleStylesheetWithOnePageOneSectionOneQuestionAndWidget_WillSucceed()
        {
            var stylesheetTask = new StylesheetTask(TestDataResolver.LoadTestFile("onePageStylesheetWithQuestionAndWidget.qls"), new List<string>());
            Node ast = _parsingService.Process(stylesheetTask).Ast;

            _assertVisitor.EnqueueWidgetNodeCallback(w =>
            {
                Assert.AreEqual(WidgetType.Radio, w.WidgetType);
            });
            ast.Accept(_assertVisitor);
            _assertVisitor.VerifyAll();
        }
    }
}
