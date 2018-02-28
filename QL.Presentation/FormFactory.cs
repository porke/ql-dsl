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

        public IList<UIElement> CreateControls(string question)
        {
            if (string.IsNullOrEmpty(question))
            {
                return new List<UIElement>();
            }

            var parsedSymbols = _parsingService.ParseQLInput(question);            
            if (parsedSymbols.Errors.Count > 0)
            {
                throw new QuestionnaireParsingException(parsedSymbols.Errors);
            }

            var formBuildingVisitor = new FormBuildingVisitor();
            parsedSymbols.FormNode.Accept(formBuildingVisitor);
            return formBuildingVisitor.Controls;
        }
    }
}
