using ReactiveUI;
using System.Windows.Input;

namespace QL.Presentation.ViewModels
{
    internal class FormViewModel
    {
        private string _formName;
        private IReactiveList<QuestionViewModel> _questions = new ReactiveList<QuestionViewModel>();

        internal FormViewModel()
        {
            _formName = string.Empty;
        }

        internal FormViewModel(string formName)
        {
            _formName = formName;
        }

        public string FormName => _formName;

        public IReactiveList<QuestionViewModel> Questions
        {
            get { return _questions; }
        }

        public ICommand QuestionValueAssignedCommand { get; set; }
    }
}
