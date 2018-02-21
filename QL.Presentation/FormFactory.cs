using QL.Core.Api;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QL.Presentation
{
    internal class FormFactory : IFormFactory
    {
        private readonly IParsingService _parsingService;

        internal FormFactory(IParsingService parsingService)
        {
            _parsingService = parsingService;
        }

        public IList<Control> CreateControls(string question)
        {
            var parsedSymbols = _parsingService.ParseQLInput(question);

            IList<Control> createdControls = new List<Control>();            
            createdControls.Add(new Label { Content = parsedSymbols.Form.Label, FontWeight = FontWeights.Bold });            

            // Needs to be replaced.
            /*parsedSymbols.Questions.ToList().ForEach(x =>
            {
                createdControls.Add(new Label { Content = x.Description });
            });*/

            return createdControls;
        }
    }
}
