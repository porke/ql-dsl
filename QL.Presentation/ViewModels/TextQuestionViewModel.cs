using ReactiveUI;
using System;

namespace QL.Presentation.ViewModels
{
    internal class TextQuestionViewModel : QuestionViewModel
    {
        private string _value;

        internal TextQuestionViewModel(string description, string id, bool isEvaluated, string value, FormViewModel parentForm) 
            : base(description, id, isEvaluated, parentForm)
        {
            _value = value;

            this.ObservableForProperty(x => x.Value)
                .Subscribe(v => _parentForm.QuestionValueAssignedCommand.Execute(this));
        }

        public string Value
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }
    }
}
