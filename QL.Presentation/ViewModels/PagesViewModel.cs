using ReactiveUI;

namespace Presentation.ViewModels
{
    internal class PagesViewModel : ReactiveObject
    {
        public IReactiveList<PageViewModel> Pages { get; } = new ReactiveList<PageViewModel>();
    }
}
