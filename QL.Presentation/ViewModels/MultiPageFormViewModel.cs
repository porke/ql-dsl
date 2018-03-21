using ReactiveUI;
using System.Collections.Generic;

namespace Presentation.ViewModels
{
    internal class MultiPageFormViewModel : FormViewModel
    {
        private int _selectedPage;

        public MultiPageFormViewModel(string name, IReadOnlyList<PageViewModel> pages) : base(name)
        {
            Pages = new ReactiveList<PageViewModel>(pages);
        }

        public int SelectedPage
        {
            get { return _selectedPage; }
            set { this.RaiseAndSetIfChanged(ref _selectedPage, value); }
        }

        public ReactiveList<PageViewModel> Pages { get; set; }
    }
}
