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
        public void EmptyFormWithConstantLabelParsesSuccessfully()
        {
            QLParser parser = Setup("form test {}");
            QLParser.FormContext context = parser.form();
            var visitor = new QLVisitor();

            Assert.IsTrue((bool)visitor.Visit(context));
        }

        [TestMethod]
        public void EmptyFormWithConstantLabelFailedParsing()
        {
            QLParser parser = Setup("form tets {}");
            QLParser.FormContext context = parser.form();
            var visitor = new QLVisitor();

            Assert.IsFalse((bool)visitor.Visit(context));
        }
    }
}
