using QL.Core.Api;
using QL.Presentation.ViewModels;
using System;
using System.Linq;

namespace QL.Presentation.Controllers
{
    internal class MainController
    {        
        private readonly IParsingService _parsingService;
        private MainViewModel _mainViewModel;

        public MainController(MainViewModel viewModel, IParsingService parsingService)
        {
            _mainViewModel = viewModel;
            _parsingService = parsingService;

            viewModel.RebuildQuestionnaireCommand = new RelayCommand(RebuildQuestionnaireCommand_Execute);
        }

        private void RebuildQuestionnaireCommand_Execute(object target)
        {            
            var parsedSymbols = _parsingService.ParseQLInput(_mainViewModel.QuestionnaireInput);
            if (parsedSymbols.Errors.Count > 0)
            {
                _mainViewModel.QuestionnaireValidation = parsedSymbols.Errors.Aggregate(
                $"Validation failed! There are {parsedSymbols.Errors.Count} error(s) in your questionnaire.",
                (err, acc) => err + Environment.NewLine + acc); 
            }
            _mainViewModel.QuestionnaireValidation = "Validation succeeded! Enjoy your questionnaire";

            var formBuildingVisitor = new FormBuildingVisitor();
            parsedSymbols.FormNode.Accept(formBuildingVisitor);
            _mainViewModel.Form = formBuildingVisitor.Form;
        }
    }
}
