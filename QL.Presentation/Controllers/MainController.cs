using QL.Api;
using QL.Api.Ast;
using QL.Api.Entities;
using QL.Api.Infrastructure;
using QL.Api.Types;
using QL.Presentation.ViewModels;
using System;
using System.Linq;

namespace QL.Presentation.Controllers
{
    internal class MainController
    {        
        private readonly Pipeline<ParsingTask> _parsingPipeline;
        private readonly Pipeline<InterpretingTask> _interpretingPipeline;
        private readonly MainViewModel _mainViewModel;
        private MemorySystem _memory;
        private SymbolTable _symbols;

        public MainController(MainViewModel viewModel, Pipeline<ParsingTask> parsingPipeline, Pipeline<InterpretingTask> interpretingPipeline)
        {
            _mainViewModel = viewModel;
            _parsingPipeline = parsingPipeline;
            _interpretingPipeline = interpretingPipeline;

            viewModel.RebuildQuestionnaireCommand = new RelayCommand<string>(RebuildQuestionnaireCommand_Execute);
        }

        private void RebuildQuestionnaireCommand_Execute(string questionnaireInput)
        {            
            var parsingTask = _parsingPipeline.Process(new ParsingTask(questionnaireInput));
            if (parsingTask.Errors.Count > 0)
            {
                _mainViewModel.QuestionnaireValidation = parsingTask.Errors.Aggregate(
                $"Validation failed! There are {parsingTask.Errors.Count} error(s) in your questionnaire.",
                (err, acc) => err + Environment.NewLine + acc);
                return;
            }
            _symbols = parsingTask.SymbolTable;
            _mainViewModel.QuestionnaireValidation = "Validation succeeded! Enjoy your questionnaire";

            _memory = new MemorySystem();
            RebuildQuestionnaire(parsingTask.Ast);
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
            
            var parsingTask = _parsingPipeline.Process(new ParsingTask(_mainViewModel.QuestionnaireInput));
            RebuildQuestionnaire(parsingTask.Ast);
        }

        private void RebuildQuestionnaire(Node ast)
        {
            var interpretingTask = _interpretingPipeline.Process(new InterpretingTask(ast, _memory, _symbols));

            var formBuildingVisitor = new FormViewModelBuildingVisitor();
            interpretingTask.InterpretedAst.Accept(formBuildingVisitor);
            _mainViewModel.Form = formBuildingVisitor.Form;
            _mainViewModel.Form.QuestionValueAssignedCommand = new RelayCommand<QuestionViewModel>(QuestionValueAssignedCommand_Execute);
        }
    }
}
