using System.Linq;
using Antlr4.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QL.Core.Test
{
    [TestClass]
    public class BasicQlTests
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

            var visitor = new QLVisitor();
            visitor.Visit(context);

            Assert.AreEqual(1, visitor.Forms.Count);
            Assert.AreEqual("test", visitor.Forms[0].Label);
            Assert.IsFalse(visitor.Forms[0].Statements.Any());
        }
    }
}
