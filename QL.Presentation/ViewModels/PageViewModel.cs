namespace Presentation.ViewModels
{
    internal class PageViewModel
    {
        internal PageViewModel(string label)
        {
            Label = label;
        }

        public SectionsViewModel Sections { get; } = new SectionsViewModel();

        public string Label { get; }
    }
}
