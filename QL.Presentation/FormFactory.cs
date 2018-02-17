using QL.Core.Api;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QL.Presentation
{
    internal class FormFactory : IFormFactory
    {
        private readonly IQLParsingService _parsingService;

        internal FormFactory(IQLParsingService parsingService)
        {
            _parsingService = parsingService;
        }

        public IList<Control> CreateControls(string question)
        {
            var parsedSymbols = _parsingService.ParseQLInput(question);

            IList<Control> createdControls = new List<Control>();

            if (parsedSymbols.Forms.Count > 0)
            {
                createdControls.Add(new Label { Content = parsedSymbols.Forms[0].Label, FontWeight = FontWeights.Bold });
            }

            parsedSymbols.Questions.ToList().ForEach(x =>
            {
                createdControls.Add(new Label { Content = x.Description });
            });

            return createdControls;
        }
    }
}
