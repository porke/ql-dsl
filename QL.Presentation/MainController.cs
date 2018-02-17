using System;
using System.Windows.Controls;

namespace QL.Presentation
{
    internal class MainController
    {
        private IFormConstructor _formConstructor;

        private MainViewModel _mainViewModel;

        public MainController(MainViewModel viewModel, IFormConstructor formConstructor)
        {
            _mainViewModel = viewModel;
            _formConstructor = formConstructor;

            viewModel.RebuildQuestionnaireCommand = new RelayCommand(RebuildQuestionnaireCommand_Execute);
        }

        private void RebuildQuestionnaireCommand_Execute(object target)
        {
            _mainViewModel.QuestionnaireHost.Children.Clear();
            _mainViewModel.QuestionnaireHost.Children.Add(_formConstructor.MakeControlFromQuestion(_mainViewModel.QuestionnaireInput));
            _mainViewModel.QuestionnaireValidation = "Validation succeeded! Enjoy your form!";
        }
    }
}
