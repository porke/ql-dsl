using QL.Core.Api;
using System.Collections.Generic;
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
            var formBuildingVisitor = new FormBuildingVisitor();
            parsedSymbols.Form.Accept(formBuildingVisitor);
            return formBuildingVisitor.Controls;
        }
    }
}
