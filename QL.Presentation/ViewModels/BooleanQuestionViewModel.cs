using ReactiveUI;

namespace QL.Presentation.ViewModels
{
    internal class BooleanQuestionViewModel : QuestionViewModel
    {
        private bool _value;

        public BooleanQuestionViewModel(string label, bool isEvaluated, bool value) : base(label, isEvaluated)
        {
            _value = value;
        }

        public bool Value
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }
    }
}
