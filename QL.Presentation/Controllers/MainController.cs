using QL.Core.Api;
using QL.Presentation.ViewModels;
using System;
using System.Linq;

namespace QL.Presentation.Controllers
{
    internal class MainController
    {        
        private readonly IParsingService _parsingService;
        private readonly IInterpretingService _interpretingService;
        private MainViewModel _mainViewModel;

        public MainController(MainViewModel viewModel, IParsingService parsingService, IInterpretingService interpretingService)
        {
            _mainViewModel = viewModel;
            _parsingService = parsingService;
            _interpretingService = interpretingService;

            viewModel.RebuildQuestionnaireCommand = new RelayCommand<string>(RebuildQuestionnaireCommand_Execute);
        }

        private void RebuildQuestionnaireCommand_Execute(string questionnaireInput)
        {            
            var parsedSymbols = _parsingService.ParseQLInput(questionnaireInput);
            if (parsedSymbols.Errors.Count > 0)
            {
                _mainViewModel.QuestionnaireValidation = parsedSymbols.Errors.Aggregate(
                $"Validation failed! There are {parsedSymbols.Errors.Count} error(s) in your questionnaire.",
                (err, acc) => err + Environment.NewLine + acc);
                return;
            }
            _mainViewModel.QuestionnaireValidation = "Validation succeeded! Enjoy your questionnaire";

            var formBuildingVisitor = new FormBuildingVisitor();
            parsedSymbols.FormNode.Accept(formBuildingVisitor);
            _mainViewModel.Form = formBuildingVisitor.Form;
            _mainViewModel.Form.QuestionValueAssignedCommand = new RelayCommand<QuestionViewModel>(QuestionValueAssignedCommand_Execute);
        }

        private void QuestionValueAssignedCommand_Execute(object target)
        {
            var questionViewModel = target as QuestionViewModel;

            _interpretingService.AssignValue(questionViewModel.Id, string.Empty);

            // TODO: 
            // 1. use the interpreter service to update the model
            // 2. grab the data from the memory model
            // 3. reconcile the view models
        }
    }
}
