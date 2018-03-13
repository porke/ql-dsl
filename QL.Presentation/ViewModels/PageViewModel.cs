using ReactiveUI;

namespace Presentation.ViewModels
{
    internal class PageViewModel
    {
        internal PageViewModel(string label)
        {
            Label = label;
        }

        public IReactiveList<QuestionViewModel> Questions { get; } = new ReactiveList<QuestionViewModel>();

        public string Label { get; }
    }
}
