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

        internal void Reconcile(FormViewModel form)
        {
            _formName = form._formName;
            for (int i = 0; i < form._questions.Count; ++i)
            {
                _questions[i].Reconcile(form._questions[i]);
            }
        }

        public string FormName => _formName;

        public IReactiveList<QuestionViewModel> Questions
        {
            get { return _questions; }
        }

        public ICommand QuestionValueAssignedCommand { get; set; }
    }
}
