using System;
using System.Windows.Controls;

namespace QL.Presentation
{
    internal class MainController
    {
        private MainViewModel _mainViewModel;

        public MainController(MainViewModel viewModel)
        {
            _mainViewModel = viewModel;

            viewModel.RebuildQuestionnaireCommand = new RelayCommand(RebuildQuestionnaireCommand_Execute);
        }

        private void RebuildQuestionnaireCommand_Execute(object target)
        {
            _mainViewModel.QuestionnaireHost.Children.Add(new Label { Content = "So, yeah, this is our questionnaire..."});
        }
    }
}
