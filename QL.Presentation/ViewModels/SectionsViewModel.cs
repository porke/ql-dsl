using ReactiveUI;

namespace Presentation.ViewModels
{
    internal class SectionsViewModel
    {
        public IReactiveList<SectionViewModel> Sections { get; } = new ReactiveList<SectionViewModel>();
    }
}
