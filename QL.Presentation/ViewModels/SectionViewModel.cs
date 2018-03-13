using ReactiveUI;

namespace Presentation.ViewModels
{
    internal class SectionViewModel
    {
        public SectionViewModel(string label)
        {
            Label = label;
        }

        public IReactiveList<QuestionViewModel> Questions { get; } = new ReactiveList<QuestionViewModel>();

        public string Label { get; }
    }
}
