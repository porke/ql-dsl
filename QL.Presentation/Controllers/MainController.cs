using QL.Api.Ast;
using QL.Api.Entities;
using QL.Api.Infrastructure;
using QL.Api.Types;
using Presentation.ViewModels;
using QLS.Api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Presentation.Visitors;

namespace Presentation.Controllers
{
    internal class MainController
    {        
        private readonly QL.Api.Infrastructure.Pipeline<ParsingTask> _parsingPipeline;
        private readonly QL.Api.Infrastructure.Pipeline<InterpretingTask> _interpretingPipeline;
        private readonly QLS.Api.Infrastructure.Pipeline<StylesheetTask> _stylesheetPipeline;

        private readonly MainViewModel _mainViewModel;
        private MemorySystem _memory;
        private SymbolTable _symbols;

        public MainController(MainViewModel viewModel,
                              QL.Api.Infrastructure.Pipeline<ParsingTask> parsingPipeline,
                              QL.Api.Infrastructure.Pipeline<InterpretingTask> interpretingPipeline,
                              QLS.Api.Infrastructure.Pipeline<StylesheetTask> stylesheetPipeline)
        {
            _mainViewModel = viewModel;
            _parsingPipeline = parsingPipeline;
            _interpretingPipeline = interpretingPipeline;
            _stylesheetPipeline = stylesheetPipeline;

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
            
            // TODO: Why rebuild the ast all the time, perhaps replace the parsing pipeline here with interpreting pipeline
            var parsingTask = _parsingPipeline.Process(new ParsingTask(_mainViewModel.QuestionnaireInput));
            RebuildQuestionnaire(parsingTask.Ast);
        }

        private void RebuildQuestionnaire(Node ast)
        {
            _mainViewModel.Form = CreateFormViewModelFromQL(ast);
            _mainViewModel.Form.Pages = CreatePagesFromStylesheet();
        }

        private FormViewModel CreateFormViewModelFromQL(Node ast)
        {
            var interpretingTask = _interpretingPipeline.Process(new InterpretingTask(ast, _memory, _symbols));

            var formBuildingVisitor = new QuestionnaireVisitor();
            interpretingTask.InterpretedAst.Accept(formBuildingVisitor);

            FormViewModel form = formBuildingVisitor.Form;
            form.QuestionValueAssignedCommand = new RelayCommand<QuestionViewModel>(QuestionValueAssignedCommand_Execute);
            return form;
        }

        private PagesViewModel CreatePagesFromStylesheet()
        {
            IReadOnlyList<QuestionViewModel> questionViewModels = _mainViewModel.Form.Questions.ToList();
            var stylesheetTask = new StylesheetTask(_mainViewModel.StylesheetInput, questionViewModels.Select(x => x.Id).ToList());
            var processedStylesheet = _stylesheetPipeline.Process(stylesheetTask);

            var stylesheetVisitor = new StylesheetVisitor(questionViewModels);
            processedStylesheet.Ast.Accept(stylesheetVisitor);
            return stylesheetVisitor.PagesViewModel;
        }
    }
}
