using QL.Api;
using QL.Api.Ast;
using QL.Api.Entities;
using QL.Api.Types;
using QL.Presentation.ViewModels;
using System;
using System.Linq;

namespace QL.Presentation.Controllers
{
    internal class MainController
    {        
        private readonly IParserService _parsingService;
        private readonly IInterpreterService _interpretingService;
        private readonly MainViewModel _mainViewModel;
        private MemorySystem _memory;
        private SymbolTable _symbols;

        public MainController(MainViewModel viewModel, IParserService parsingService, IInterpreterService interpretingService)
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
            _symbols = parsedSymbols.Symbols;
            _mainViewModel.QuestionnaireValidation = "Validation succeeded! Enjoy your questionnaire";

            _memory = new MemorySystem();
            RebuildQuestionnaire(parsedSymbols.FormNode);
        }

        private void QuestionValueAssignedCommand_Execute(object target)
        {
            var questionViewModel = target as QuestionViewModel;

            Value memoryValue;
            if(!_memory.TryRetrieveValue(questionViewModel.Id, out memoryValue))
            {
                memoryValue = new Value(_symbols[questionViewModel.Id].Type);
            }
            _memory.AssignValue(questionViewModel.Id, new Value(questionViewModel.Value, memoryValue.Type));

            Node ast = _parsingService.ParseQLInput(_mainViewModel.QuestionnaireInput).FormNode;
            RebuildQuestionnaire(ast);
        }

        private void RebuildQuestionnaire(Node ast)
        {
            Node evaluatedAst = _interpretingService.EvaluateQuestionnaire(ast, _memory, _symbols);

            var formBuildingVisitor = new FormViewModelBuildingVisitor();
            evaluatedAst.Accept(formBuildingVisitor);
            _mainViewModel.Form = formBuildingVisitor.Form;
            _mainViewModel.Form.QuestionValueAssignedCommand = new RelayCommand<QuestionViewModel>(QuestionValueAssignedCommand_Execute);
        }
    }
}
