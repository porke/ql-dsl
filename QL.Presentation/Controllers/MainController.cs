using QL.Api.Ast;
using QL.Api.Entities;
using QL.Api.Infrastructure;
using Presentation.ViewModels;
using QLS.Api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Presentation.Visitors;
using Infrastructure;
using QL.Api.Factories;
using ReactiveUI;

namespace Presentation.Controllers
{
    internal class MainController
    {        
        private readonly Pipeline<ParsingTask> _parsingPipeline;
        private readonly Pipeline<InterpretingTask> _interpretingPipeline;
        private readonly Pipeline<StylesheetTask> _stylesheetPipeline;

        private readonly MainViewModel _mainViewModel;
        private Node _qlAst;
        private MemorySystem _memory;
        private SymbolTable _symbols;
        private readonly IValueFactory _valueFactory;

        public MainController(MainViewModel viewModel,
                              Pipeline<ParsingTask> parsingPipeline,
                              Pipeline<InterpretingTask> interpretingPipeline,
                              Pipeline<StylesheetTask> stylesheetPipeline,
                              IValueFactory valueFactory)
        {
            _mainViewModel = viewModel;
            _parsingPipeline = parsingPipeline;
            _interpretingPipeline = interpretingPipeline;
            _stylesheetPipeline = stylesheetPipeline;
            _valueFactory = valueFactory;

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
            _qlAst = parsingTask.Ast;
            _memory = new MemorySystem();
            _mainViewModel.QuestionnaireValidation = "Validation succeeded! Enjoy your questionnaire.";
            
            RebuildQuestionnaire(parsingTask.Ast);
        }

        private void QuestionValueAssignedCommand_Execute(object target)
        {
            var questionViewModel = target as QuestionViewModel;

            IValue memoryValue;
            if(!_memory.TryRetrieveValue(questionViewModel.Id, out memoryValue))
            {
                memoryValue = _valueFactory.CreateDefaultValue(_symbols[questionViewModel.Id].Type);
            }
            _memory.AssignValue(questionViewModel.Id, _valueFactory.CreateValue(questionViewModel.Value, memoryValue.GetType()));                        
            RebuildQuestionnaire(_qlAst);
        }

        private void RebuildQuestionnaire(Node evaluatedAst)
        {
            SinglePageFormViewModel singlePageViewModel = CreateSinglePageFormFromQL(evaluatedAst);
            if (!string.IsNullOrEmpty(_mainViewModel.StylesheetInput))
            {
                List<PageViewModel> pageList = CreatePaginatedFormFromStylesheet(singlePageViewModel.Questions.ToList());
                if (_mainViewModel.Form is MultiPageFormViewModel)
                {
                    var multiPageViewModel = ((MultiPageFormViewModel)_mainViewModel.Form);
                    int selectedPage = multiPageViewModel.SelectedPage;
                    multiPageViewModel.Pages.Clear();
                    pageList.ForEach(x => multiPageViewModel.Pages.Add(x));
                    multiPageViewModel.SelectedPage = selectedPage;
                }
                else
                {
                    _mainViewModel.Form = new MultiPageFormViewModel(singlePageViewModel.Name, new ReactiveList<PageViewModel>(pageList));
                }                
            }
            else
            {
                _mainViewModel.Form = singlePageViewModel;
            }
        }

        private SinglePageFormViewModel CreateSinglePageFormFromQL(Node ast)
        {
            var interpretingTask = _interpretingPipeline.Process(new InterpretingTask(ast, _memory, _symbols));

            var formBuildingVisitor = new QuestionnaireVisitor();
            interpretingTask.InterpretedAst.Accept(formBuildingVisitor);

            SinglePageFormViewModel form = formBuildingVisitor.Form;
            form.QuestionValueAssignedCommand = new RelayCommand<QuestionViewModel>(QuestionValueAssignedCommand_Execute);
            return form;
        }

        private List<PageViewModel> CreatePaginatedFormFromStylesheet(IReadOnlyList<QuestionViewModel> questions)
        {
            IReadOnlyList<QuestionViewModel> questionViewModels = _mainViewModel.Form.Questions.ToList();
            var stylesheetTask = new StylesheetTask(_mainViewModel.StylesheetInput, _symbols);
            var processedStylesheet = _stylesheetPipeline.Process(stylesheetTask);

            var stylesheetVisitor = new StylesheetVisitor(questions);
            processedStylesheet.Ast.Accept(stylesheetVisitor);

            return stylesheetVisitor.PageViewModels;
        }
    }
}
