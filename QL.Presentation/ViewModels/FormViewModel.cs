using ReactiveUI;
using System.Windows.Input;

namespace Presentation.ViewModels
{
    internal class FormViewModel : ReactiveObject
    {
        private string _formName;
        private PagesViewModel _pagesViewModel;

        internal FormViewModel()
        {
            _formName = string.Empty;
        }

        internal FormViewModel(string formName)
        {
            _formName = formName;
        }

        public string FormName => _formName;

        public IReactiveList<QuestionViewModel> Questions { get; } = new ReactiveList<QuestionViewModel>();

        public PagesViewModel Pages
        {
            get { return _pagesViewModel; }
            set { this.RaiseAndSetIfChanged(ref _pagesViewModel, value); }
        }

        public ICommand QuestionValueAssignedCommand { get; set; }
    }
}
