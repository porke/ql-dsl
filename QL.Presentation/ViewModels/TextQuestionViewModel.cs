using ReactiveUI;

namespace QL.Presentation.ViewModels
{
    internal class TextQuestionViewModel : QuestionViewModel
    {
        private string _value;

        internal TextQuestionViewModel(string label, bool isEvaluated, string value) : base(label, isEvaluated)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }
    }
}
