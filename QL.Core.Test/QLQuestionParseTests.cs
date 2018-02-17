using System.Linq;
using Antlr4.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Parsing;
using System.IO;
using System.Reflection;
using static QL.Core.QLParser;

namespace QL.Core.Test
{
    [TestClass]
    public class QLQuestionParseTests
    {
        private QLParser Setup(string text)
        {
            var inputStream = new AntlrInputStream(text);
            var speakLexer = new QLLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(speakLexer);

            return new QLParser(commonTokenStream);
        }

        private string LoadTestFile(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceId = assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(resourceName));
            if (string.IsNullOrEmpty(resourceId))
            {
                return string.Empty;
            }

            using (Stream stream = assembly.GetManifestResourceStream(resourceId))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }            
        }

        [TestMethod]
        public void ParseOneQuestionWithALabelNoStatements_WillSucceed()
        {
            var parser = Setup(LoadTestFile("BasicQuestion.ql"));
            QuestionContext context = parser.question();
            var visitor = new QLQuestionVisitor();

            visitor.Visit(context);
            
            Assert.AreEqual(1, visitor.Questions.Count);
            Assert.AreEqual("whatIsMeaning", visitor.Questions[0].Label);
            Assert.AreEqual("What is the meaning of life?", visitor.Questions[0].Description);
            //Assert.AreEqual("money", visitor.Questions[0].Type);
        }
    }
}
