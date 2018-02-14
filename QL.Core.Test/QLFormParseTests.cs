using System.Linq;
using Antlr4.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Parsing;

namespace QL.Core.Test
{
    [TestClass]
    public class QLFormParseTests
    {
        private QLParser Setup(string text)
        {
            var inputStream = new AntlrInputStream(text);
            var speakLexer = new QLLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(speakLexer);

            return new QLParser(commonTokenStream);
        }

        [TestMethod]
        public void ParseEmptyFormWithNoStatements_WillSucceed()
        {
            QLParser parser = Setup("form test {}");
            QLParser.FormContext context = parser.form();

            var visitor = new QLFormVisitor();
            visitor.Visit(context);

            Assert.AreEqual(1, visitor.Forms.Count);
            Assert.AreEqual("test", visitor.Forms[0].Label);
            Assert.IsFalse(visitor.Forms[0].Statements.Any());
        }
    }
}
