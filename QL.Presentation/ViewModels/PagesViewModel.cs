using ReactiveUI;

namespace Presentation.ViewModels
{
    internal class PagesViewModel : ReactiveObject
    {
        private int _selectedPage = -1;      

        public int SelectedPage
        {
            get { return _selectedPage; }
            set { this.RaiseAndSetIfChanged(ref _selectedPage, value); }
        }

        public IReactiveList<PageViewModel> Pages { get; } = new ReactiveList<PageViewModel>();
    }
}
