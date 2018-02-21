using System;
using System.Linq;

namespace QL.Presentation
{
    internal class MainController
    {
        private IFormFactory _formFactory;
        private MainViewModel _mainViewModel;

        public MainController(MainViewModel viewModel, IFormFactory formConstructor)
        {
            _mainViewModel = viewModel;
            _formFactory = formConstructor;

            viewModel.RebuildQuestionnaireCommand = new RelayCommand(RebuildQuestionnaireCommand_Execute);
        }

        private void RebuildQuestionnaireCommand_Execute(object target)
        {
            _mainViewModel.QuestionnaireHost.Children.Clear();            

            try
            {
                _formFactory.CreateControls(_mainViewModel.QuestionnaireInput).ToList().ForEach(x =>
                {
                    _mainViewModel.QuestionnaireHost.Children.Add(x);
                });
                _mainViewModel.QuestionnaireValidation = "Validation succeeded! Enjoy your questionnaire";
            }
            catch (QuestionnaireParsingException e)
            {
                _mainViewModel.QuestionnaireValidation = e.Errors.Aggregate(
                    $"Validation failed! There are {e.Errors.Count} error(s) in your questionnaire.",
                    (err, acc) => err + Environment.NewLine + acc);
            }            
        }
    }
}
