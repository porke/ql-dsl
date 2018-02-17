using QL.Core.Api;
using System.Windows.Controls;

namespace QL.Presentation
{
    internal class FormConstructor : IFormConstructor
    {
        private readonly IQLParsingService _parsingService;

        internal FormConstructor(IQLParsingService parsingService)
        {
            _parsingService = parsingService;
        }

        public Control MakeControlFromQuestion(string question)
        {
            _parsingService.ParseQLInput(question);            

            return new Label { Content = _parsingService.Questions[0].Description};
        }
    }
}
