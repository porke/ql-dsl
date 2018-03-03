using ReactiveUI;

namespace QL.Presentation.ViewModels
{
    internal abstract class QuestionViewModel : ReactiveObject
    {
        private string _description;
        private string _id;
        private bool _isEvaluated;
        protected FormViewModel _parentForm;

        internal QuestionViewModel(string description, string id, bool isEvaluated, FormViewModel parentForm)
        {
            _description = description;
            _isEvaluated = isEvaluated;
            _parentForm = parentForm;
        }

        public void Reconcile(QuestionViewModel question)
        {
            _description = question._description;
            _isEvaluated = question._isEvaluated;
        }

        public string Description => _description;

        public string Id => _id;

        public bool IsEvaluated => _isEvaluated;
    }
}
