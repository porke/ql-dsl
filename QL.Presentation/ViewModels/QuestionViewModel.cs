using ReactiveUI;

namespace QL.Presentation.ViewModels
{
    internal abstract class QuestionViewModel : ReactiveObject
    {
        private string _label;
        private bool _isEvaluated;

        internal QuestionViewModel(string label, bool isEvaluated)
        {
            _label = label;
            _isEvaluated = isEvaluated;
        }

        public string Label => _label;

        public bool IsEvaluated => _isEvaluated;
    }
}
