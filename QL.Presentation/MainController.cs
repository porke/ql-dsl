using System;

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
            throw new NotImplementedException();
        }
    }
}
