using ReactiveUI;

namespace Presentation.ViewModels
{
    internal class SectionViewModel
    {
        private string _label;

        public SectionViewModel(string label)
        {
            _label = label;
        }

        public IReactiveList<QuestionViewModel> Questions { get; } = new ReactiveList<QuestionViewModel>();
    }
}
