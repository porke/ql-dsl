using System;
using ReactiveUI;

namespace Presentation.ViewModels
{
    internal abstract class QuestionViewModel : ReactiveObject
    {
        private string _description;
        private string _id;
        private bool _isEvaluated;
        private object _value;
        private FormViewModel _parentForm;

        protected QuestionViewModel(string description, string id, bool isEvaluated, object value, FormViewModel parentForm)
        {
            _id = id;
            _description = description;
            _isEvaluated = isEvaluated;
            _parentForm = parentForm;
            _value = value;

            this.ObservableForProperty(x => x.Value)
                .Subscribe(x => parentForm.QuestionValueAssignedCommand.Execute(this));
        }       

        public string Description => _description;

        public string Id => _id;

        public object Value
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }

        public bool IsEvaluated => _isEvaluated;
    }
}
