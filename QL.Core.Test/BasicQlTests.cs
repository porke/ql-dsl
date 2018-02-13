using Antlr4.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QL.Core.Test
{
    [TestClass]
    public class BasicQlTests
    {
        private SpeakParser Setup(string text)
        {
            var inputStream = new AntlrInputStream(text);
            var speakLexer = new SpeakLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(speakLexer);

            return new SpeakParser(commonTokenStream);
        }

        [TestMethod]
        public void WillSucceed()
        {
            SpeakParser parser = Setup("john says hello \n michael says world \n");

            SpeakParser.ChatContext context = parser.chat();
            SpeakVisitor visitor = new SpeakVisitor();
            visitor.Visit(context);

            Assert.AreEqual(2, visitor.Lines.Count);
        }
    }
}
